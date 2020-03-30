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
    
    
    private float[] time;
    private bool _isGameover = false;
    public bool IsGameOver
    {
        get { return _isGameover; }
        set
        {
            if (MissionTask.Bool_Gameover == _isGameover)
                return;

            _isGameover = MissionTask.Bool_Gameover;
            if (_isGameover)
            {
                Obj_GameOver.SetActive(true);
            }
        }
    }

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
