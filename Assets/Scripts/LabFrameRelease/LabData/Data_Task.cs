using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LabData;
using DataSync;

public class Data_TaskS : LabDataBase
{
    public Data_TaskS() { }

    public Dictionary<string, TaskData> data_TasksDic = new Dictionary<string, TaskData>();
}

public class TaskData : LabDataBase
{
    public TaskData() { }

    public List<OriginCubeData> List_originCubeData;
    public List<CubeParentData> List_cubeParentData;
    public List<TriggerParentData> List_triggerParentData;
}



public class CubeParentData
{
    public CubeParentData() { }

    public string parentName;

    public float[] parentPos;
    public float[] parentRot;
    public float[] parentScl;

    public List<CubeData> List_cubeData;
}

public class CubeData
{
    public CubeData() { }

    public string cubeName;

    public float[] cubePos;
    public float[] cubeRot;
    public float[] cubeScl;
    public float[] cubeColorRGBA;
}

public class OriginCubeData
{
    public OriginCubeData() { }

    public string originCubeName;

    public float[] originCubePos;
    public float[] originCubeColorRGBA;
}

public class TriggerParentData
{
    public TriggerParentData() { }

    public string triggerParentName;

    public float[] triggerParentPos;

    public List<TriggerData> List_triggerData;
}

public class TriggerData
{
    public TriggerData() { }

    public string TriggerName;

    public float[] TriggerPos;
    public float[] TriggerColorRGBA;
    public float[] TriggerScl;
}