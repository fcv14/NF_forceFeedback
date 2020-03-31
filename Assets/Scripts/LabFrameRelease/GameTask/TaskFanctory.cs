using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TestGameFrame;

public class TaskFanctory 
{
    public static List<TaskBase> GetCurrentScopeTasks()
    {
        var temptasks = new List<TaskBase>
        {
            new MissionTask()
        };
        return temptasks;
    }

    public static List<TaskBase> GetBuildTask()
    {
        var temptasks = new List<TaskBase>
        {
            new BuildTask()
        };
        return temptasks;
    }

    public static List<TaskBase> GetEditTask()
    {
        var temptasks = new List<TaskBase>
        {
            new EditTask()
        };
        return temptasks;
    }
}
