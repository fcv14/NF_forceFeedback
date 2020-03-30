using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public GameObject Obj_timeTable;
    public Text Txt_Stage;
    public Text Txt_time;

    public GameObject Obj_Okay_NextTask;
    public Button Btn_Okay_NextTask;

    public GameObject Obj_GameOver;
    public Text Txt_totalTime;
    public Button Btn_Exit;
    //完成GameUI後 開始導入 力反饋 與完成ALL TRIGGER ENTER


    private float[] time;
    void Start()
    {
        Btn_Okay_NextTask.onClick.AddListener(delegate { NextTask(); });
        Btn_Exit.onClick.AddListener(delegate { Exit(); });

    }

    void Update()
    {
        
    }

    void NextTask()
    {

    }

    void Exit()
    {

    }

}
