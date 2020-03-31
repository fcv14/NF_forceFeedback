using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LabData;
using DataSync;

public class Data_Output : LabDataBase
{
    public Data_Output(float[] tasktimes,float totaltime) {
        TaskTimes = tasktimes;
        TotalTime = totaltime;
    }

    public float[] TaskTimes { get; set; }
    public float TotalTime { get; set; }
}
