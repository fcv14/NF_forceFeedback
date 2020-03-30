using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LabData;
using DataSync;

public class Data_Missions : LabDataBase
{
    public Data_Missions() { }
    
    //              missionName taskNameList
    public Dictionary<string, List<string>> Dic_missions = new Dictionary<string, List<string>>();
}
