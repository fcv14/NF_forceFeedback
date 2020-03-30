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
        public Data_Missions Missions { get; set; }//還沒用


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
        /// FlowData 构造函数
        /// </summary>
        public GameFlowData(string userID , Data_TaskS data_tasks)
        {
            UserId = userID;
            DataTasks = data_tasks;
        }

        public GameFlowData(string taskname)
        {
            TaskName = taskname;
        }

        public GameFlowData(string userID ,List<string> list_tasksName)
        {
            UserId = userID;
            List_tasksName = list_tasksName;
        }


        public GameFlowData() { }
    }
}