using System.Collections;
using System.Collections.Generic;
using GameData;
using LabData;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    public InputField userID;
    public Dropdown missionDropdown;
    public Button load_Mission_Btn;
    public Button del_Mission_Btn;
    public Button build_Misson_Btn;

    private void Start()
    {
        GameDataManager.FlowData = new GameFlowData();
        userID.text = "s";
        GameDataManager.FlowData.UserId = userID.text;
    }

}
