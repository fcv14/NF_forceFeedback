using System.Collections;
using System.Collections.Generic;
using System.Linq;//for dic.keys.ToList()
using GameData;
using LabData;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    [Header("Start_UI")]
    public GameObject Obj_StartUI;
    public InputField IPF_userID;
    public Dropdown Dropdown_missionName;
    public Button Btn_loadMission;
    public Button Btn_deleteMission;
    public Button Btn_EnterMissionName;
    [Space(10)]
    public GameObject Obj_After_buildClicked;
    public InputField IPF_newMissionName;
    public Button Btn_GO2buildMission;
    

    [Header("MissionBuild_UI")]
    public GameObject Obj_MissionCreateUI;
    //public InputField IPF_TaskNumForMission;
    public Button Btn_TaskAdd2Mission;
    public Button Btn_BuildMissionConfirm;
    public Dropdown Dropdown_taskName;
    public Button Btn_EditTask;
    public Button Btn_DeleteTask;
    public Button Btn_Back2StartUI;
    public Button Btn_BuildTask;
    public GameObject Obj_MissionCreateSuccessfully;
    public Button Btn_Okay_MissionCreateSuccessfully;

    [Header("whileInputNull_UI")]
    public GameObject Obj_WhileInputNull;
    public Button Btn_Okay_WhileInputNull;

    public List<string> List_taskAdd = new List<string>();
    public Data_TaskS data_tasks = new Data_TaskS();
    //public Data_Mission data_thisMission = new Data_Mission();
    public Data_Missions data_Missions = new Data_Missions();


    private void Start()
    {
        //GameDataManager.FlowData = new GameFlowData(userID.text);

        //StartUI Buttons
        Btn_loadMission.onClick.AddListener(delegate { Load_Mission(); });
        Btn_deleteMission.onClick.AddListener(delegate { Delete_Mission(); });
        Btn_EnterMissionName.onClick.AddListener(delegate { EnterMissionName(); });
        Btn_GO2buildMission.onClick.AddListener(delegate { GO2Build_MissionUI(); });

        //MissionBuildUI Buttons
        Btn_TaskAdd2Mission.onClick.AddListener(delegate{ TaskAdd2Mission(); });
        Btn_BuildMissionConfirm.onClick.AddListener(delegate { Build_Mission(); });
        Btn_EditTask.onClick.AddListener(delegate { Edit_Task(); });
        Btn_DeleteTask.onClick.AddListener(delegate { Delete_Task(); });
        Btn_Back2StartUI.onClick.AddListener(delegate { Back2StartUI(); });
        Btn_BuildTask.onClick.AddListener(delegate { Build_Task(); });
        Btn_Okay_MissionCreateSuccessfully.onClick.AddListener(delegate { Okay_MissonBuildSuc(); });

        //NullInput Buttons
        Btn_Okay_WhileInputNull.onClick.AddListener(delegate { Okay_HintObjFunction(); });

        
        //載入任務dropdown List
        //Data_missionname data_Missionname = LabTools.GetData<Data_missionname>("missionName");

        //Dictionary<string, string> Dic_missions = data_Missionname.mission_Dic;

        //Dropdown_missionName.ClearOptions();

        //Dropdown_missionName.AddOptions(Dic_missions.Keys.ToList());

        //載入Task dropdown List
        data_tasks = LabTools.GetData<Data_TaskS>("AllTasks");
        Dropdown_taskName.ClearOptions();
        Dropdown_taskName.AddOptions(data_tasks.data_TasksDic.Keys.ToList());
        //載入Mission dropdown List
        data_Missions = LabTools.GetData<Data_Missions>("AllMissions");
        Dropdown_missionName.ClearOptions();
        Dropdown_missionName.AddOptions(data_Missions.Dic_missions.Keys.ToList());
    }
    

    void Load_Mission()
    {
        //輸入使用者名稱才能
        //將抓取到的mission做loading
        //Dropdown_missionName.captionText.text

        string missionName = Dropdown_missionName.captionText.text;
        GameFlowData gameFlow = new GameFlowData(IPF_userID.text,data_Missions.Dic_missions[missionName]); //得到對應value值 : Tasks的String
        GameDataManager.FlowData = gameFlow;
        var Id = gameFlow.UserId;
        GameDataManager.LabDataManager.LabDataCollectInit(() => Id);
        GameSceneManager.Instance.Change2GameScene();

    }

    void Delete_Mission()
    {
        
        
    }

    void EnterMissionName()
    {
        if (Obj_After_buildClicked.activeSelf)
        {
            Obj_After_buildClicked.SetActive(false);
        }
        else
        {
            Obj_After_buildClicked.SetActive(true);
        }
    }

    void GO2Build_MissionUI()
    {
        if ((IPF_userID.text != "") && (IPF_newMissionName.text!= ""))
        {
            //MissionUI
            Obj_MissionCreateUI.SetActive(true);
            Obj_After_buildClicked.SetActive(false);
        }
        else
        {
            SetUI_Interactable(Obj_StartUI, false);
            Obj_WhileInputNull.SetActive(true);
            Obj_WhileInputNull.GetComponentInChildren<Text>().text = "請輸入 User ID 及 任務名稱";
        }
    }

    void Okay_HintObjFunction()//inputNull後的跳出提示視窗
    {
        if (Obj_StartUI.activeSelf)
        {
            Obj_WhileInputNull.SetActive(false);
            SetUI_Interactable(Obj_StartUI, true);
        }

        if (Obj_MissionCreateUI.activeSelf)
        {
            Obj_WhileInputNull.SetActive(false);
            SetUI_Interactable(Obj_MissionCreateUI, true);
        }
    }

    void TaskAdd2Mission()
    {
        List_taskAdd.Add(Dropdown_taskName.captionText.text);
    }

    void Build_Mission()
    {
        //存檔
        //輸入TaskNumString
        if ( List_taskAdd.Count == 0)//為空
        {
            Obj_WhileInputNull.GetComponentInChildren<Text>().text = "請選擇至少一關卡加入任務";
            Obj_WhileInputNull.SetActive(true);
            SetUI_Interactable(Obj_MissionCreateUI, false);
        }
        else//不為空
        {
            //跳出建立完成視窗
            //執行任務創建工作
            data_Missions.Dic_missions.Add(IPF_newMissionName.text, List_taskAdd);
            
            LabTools.CreateDataFolder<Data_Missions>();
            LabTools.WriteData<Data_Missions>(data_Missions, "AllMissions", true);

            data_Missions = LabTools.GetData<Data_Missions>("AllMissions");
            Dropdown_missionName.ClearOptions();
            Dropdown_missionName.AddOptions(data_Missions.Dic_missions.Keys.ToList());

            Obj_MissionCreateSuccessfully.SetActive(true);
        }

    }


    void Edit_Task()
    {
        GameFlowData gameFlow = new GameFlowData(Dropdown_taskName.captionText.text);//編輯task所需要用到的TaskName
        GameDataManager.FlowData = gameFlow;
        //var Id = gameFlow.UserId;
        //GameDataManager.LabDataManager.LabDataCollectInit(() => Id);
        GameSceneManager.Instance.Change2EditScene();
        //轉Scene
    }

    void Delete_Task()
    {
        data_tasks.data_TasksDic.Remove(Dropdown_taskName.captionText.text);

        LabTools.CreateDataFolder<Data_TaskS>();
        LabTools.WriteData<Data_TaskS>(data_tasks, "AllTasks", true);
        Dropdown_taskName.ClearOptions();
        Dropdown_taskName.AddOptions(data_tasks.data_TasksDic.Keys.ToList());
    }

    void Build_Task()
    {
        GameFlowData gameFlow = new GameFlowData(IPF_userID.text, data_tasks);//還要Input很多東西
        GameDataManager.FlowData = gameFlow;
        //var Id = gameFlow.UserId;
        //GameDataManager.LabDataManager.LabDataCollectInit(() => Id);
        GameSceneManager.Instance.Change2BuildScene();
        //轉Scene
    }

    void Back2StartUI()
    {
        Obj_MissionCreateUI.SetActive(false);
        Obj_StartUI.SetActive(true);
    }

    void Okay_MissonBuildSuc()
    {
        Obj_MissionCreateSuccessfully.SetActive(false);
    }

    void SetUI_Interactable(GameObject ParentUI,bool interactable = true)
    {
        Selectable[] selectables = ParentUI.GetComponentsInChildren<Selectable>();

        foreach (Selectable b in selectables)
        {
            b.interactable = interactable;
        }
    }
}
