using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataSync;



namespace GameData
{
    public class GameFlowData : LabDataBase
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId { get; set; } = "Test01";

        /// <summary>
        /// 任務名稱
        /// </summary>
        public string MissionsName { get; set; }

        /// <summary>
        /// 是否旋轉
        /// </summary>
        public bool Rotation { get; set; }

        /// <summary>
        /// 是否使用透明提示
        /// </summary>
        public bool TransparentHint { get; set; }

        /// <summary>
        /// Tasks
        /// </summary>
        public Data_TaskS DataTasks { get; set; }
        
       
        ///<summary>
        ///轉Edit Task要用的Task Name
        /// </summary>
        public string TaskName { get; set; }


        public List<string> List_tasksName { get; set; }

        
        /// <summary>
        /// Build要用到的FlowData
        /// </summary>
        /// <param name="data_TaskS"></param>
        public GameFlowData(Data_TaskS data_TaskS)
        {
            DataTasks = data_TaskS;
        }
        /// <summary>
        /// Edit要用到的FlowData
        /// </summary>
        /// <param name="taskname"></param>
        public GameFlowData(string taskname)
        {
            TaskName = taskname;
        }

        /// <summary>
        /// 給Game用的
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="list_tasksName"></param>
        public GameFlowData(string userID,string missionName ,List<string> list_tasksName,bool rotation,bool transparentHint)
        {
            UserId = userID;
            MissionsName = missionName;
            List_tasksName = list_tasksName;
            Rotation = rotation;
            TransparentHint = transparentHint;
        }


        public GameFlowData() { }
    }
}