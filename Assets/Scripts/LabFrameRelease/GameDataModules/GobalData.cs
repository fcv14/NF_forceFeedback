using DataSync;

namespace GameData
{
    public class GobalData : LabDataBase
    {     

        /// <summary>
        /// 游戏的主UI场景名
        /// </summary>
        public const string MainUiScene = "MainUIScene";
        public const string GameScene = "GameScene";
        public const string BuildScene = "BuildScene";
        public const string EditScene = "EditScene";
        
        public const int GameUIManagerWeight = 0;
        public const int GameEntityManagerWeight = 30;
        public const int GameSceneManagerWeight = 50;
        public const int GameTaskManagerWeight = 80;
        public const int GameDataManagerWeight = 100;


    }

}

