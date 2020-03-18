using System.Collections;
using System.Collections.Generic;
using GameData;
using LabData;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    public InputField userID;
    public Dropdown Dropdown_missionName;
    public Button Btn_loadMission;
    public Button Btn_deleteMission;
    public Button Btn_buildMission;
    
    

    private void Start()
    {
        //GameDataManager.FlowData = new GameFlowData(userID.text);

        //Buttons
        Btn_loadMission.onClick.AddListener(delegate
        {
            Load_Mission();
        });

        Btn_deleteMission.onClick.AddListener(delegate { Delete_Mission(); });
        Btn_buildMission.onClick.AddListener(delegate { Build_Mission(); });



        Data_missionname data_Missionname = LabTools.GetData<Data_missionname>("missionName");
        List<string> List_missionName = data_Missionname.missionNameData_List;

        Dropdown_missionName.ClearOptions();
        Dropdown_missionName.AddOptions(List_missionName);

    }




    void Load_Mission()
    {
        //將抓取到的mission做loading
        //Dropdown_missionName.captionText.text

        //如何loading
        //進入遊戲..loadScene  flowdata???
        //環境重建
    }

    void Delete_Mission()
    {
        missionData newMissionData = new missionData();

        taskNum_List.RemoveAt(missionDropdown.value);//將對應的tasks刪掉

        missionName_List.RemoveAt(missionDropdown.value);
        missionDropdown.ClearOptions();
        missionDropdown.AddOptions(missionName_List);

        newMissionData.taskNum_List = taskNum_List;
        newMissionData.missionNameData_List = missionName_List;

        LabTools1.CreateDataFolder<missionData>();
        LabTools1.WriteData<missionData>(newMissionData, "missionName", true);
    }

    void Build_Mission()
    {

    }
}
