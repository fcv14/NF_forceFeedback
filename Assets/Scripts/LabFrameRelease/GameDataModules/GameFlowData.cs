using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataSync;



namespace GameData
{
    public class GameFlowData : LabDataBase
    {
        ///// <summary>
        ///// 语言
        ///// </summary>
        //public Language Language { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId { get; set; } = "Test01";

        /// <summary>
        /// 任務名稱
        /// </summary>
        public string MissionName { get; set; } = "Test02";


        /// <summary>
        /// FlowData 构造函数
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="languageType"></param>
        /// <param name="remindType"></param>
        /// <param name="gameData"></param>
        public GameFlowData(string UserID,string Missionname)
        {
            UserId = UserID;
            MissionName = Missionname;
        }

        
        public GameFlowData()
        {
        }
    }
}