﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DataSync;
using LabData;


public class GameUI : MonoBehaviour
{
    public Slider Slider_ColorAlpha;
    private float f_ColorAjpha = 0.4f;

    public GameObject Obj_timeTable;
    public Text Txt_Stage;
    public Text Txt_time;

    public GameObject Obj_Okay_NextTask;
    public Text Txt_Okay_NextTask;
    public Button Btn_Okay_NextTask;

    public GameObject Obj_GameOver;
    public Text Txt_totalTime;
    public Button Btn_Exit;

    
    private float start_time;
    private float temp_time;
    private float[] time;
    int Count_Tasks;
    int i = 0;

    private float Cube_alpha;
    private int color_Num = 0;
    private Color32[] color_Array; //color陣列


    public static bool Bool_nextTask = false;

    void Start()
    {
        Btn_Okay_NextTask.onClick.AddListener(delegate { NextTask(); });
        Btn_Exit.onClick.AddListener(delegate { Exit(); });
        Slider_ColorAlpha.onValueChanged.AddListener(delegate { Slider_alphaChange(); });
        GameEventCenter.AddEvent("Show_ObjOkayNextTask",Okay_NextTaskAndTimeData);
        GameEventCenter.AddEvent("Show_ObjGameover",Okay_Gameover);
        GameEventCenter.AddEvent("Toggle_Alpha", Toggle_alphaChange);

        Count_Tasks = GameDataManager.FlowData.List_tasksName.Count;
        start_time = Time.time;
        temp_time = start_time;
        time = new float[Count_Tasks];//存各關卡結束時間

        Txt_Stage.text = (i + 1) + "/" + Count_Tasks;
    }
    private void Update()
    {
        Txt_time.text =  (Time.time - temp_time).ToString("f1");
    }

    void Okay_NextTaskAndTimeData()
    {
        float time_now = Time.time;
        Txt_Okay_NextTask.text = "完成第" + (i+1) + "關,共花費" + (time_now - temp_time).ToString("f1") + "   秒";
        Obj_Okay_NextTask.SetActive(true);
        Txt_Stage.text = (i+2) + "/" + Count_Tasks;
        time[i] = time_now - temp_time;
        i++;
    }

    void NextTask()
    {
        Bool_nextTask = true;
        Obj_Okay_NextTask.SetActive(false);
        temp_time = Time.time;
    }

    void Okay_Gameover()
    {
        Obj_Okay_NextTask.SetActive(false);
        Txt_totalTime.text = (Time.time - start_time).ToString("f2");
        Obj_GameOver.SetActive(true);

        OutputResult();
        //儲存OUTPUT DATA
        GameApplication.Instance.GameApplicationQuit();
       
    }

    void OutputResult() {
        Data_Output data_Output = new Data_Output(time, Time.time - start_time);
        GameDataManager.LabDataManager.SendData(GameDataManager.FlowData);
        GameDataManager.LabDataManager.SendData(data_Output);
    }

    void Exit()
    {
        GameSceneManager.Instance.Change2MainUI();

    }
    void Slider_alphaChange()
    {
        Cube_alpha = Slider_ColorAlpha.value;
        foreach (var trg_P in MissionTask.List_triggers)
        {
            Color C_A = new Color();
            C_A.a = Cube_alpha;
            C_A.r = trg_P.GetComponent<Renderer>().material.color.r;
            C_A.g = trg_P.GetComponent<Renderer>().material.color.g;
            C_A.b = trg_P.GetComponent<Renderer>().material.color.b;
            trg_P.transform.GetComponent<Renderer>().material.color = C_A;
        }
    }
    void Toggle_alphaChange()
    {
        if (GameDataManager.FlowData.TransparentHint) {
            Cube_alpha = f_ColorAjpha;
            foreach (var trg_P in MissionTask.List_triggers)
            {
                Color C_A = new Color();
                C_A.a = Cube_alpha;
                C_A.r = trg_P.GetComponent<Renderer>().material.color.r;
                C_A.g = trg_P.GetComponent<Renderer>().material.color.g;
                C_A.b = trg_P.GetComponent<Renderer>().material.color.b;
                trg_P.transform.GetComponent<Renderer>().material.color = C_A;
            } }
    }
}
