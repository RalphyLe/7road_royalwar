using GameFramework;
namespace RoyalWar
{
    /// <summary>
    /// 游戏入口。
    /// </summary>
    public partial class GameEntry
    {
        //public static BuiltinDataComponent BuiltinData
        //{
        //    get;
        //    private set;
        //}
        public static SoldierComponent Soldier
        {
            get;
            private set;
        }

        public static WarlordComponent Warlord
        {
            get;
            private set;
        }

        private static void InitCustomComponents()
        {
            //BuiltinData = UnityGameFramework.Runtime.GameEntry.GetComponent<BuiltinDataComponent>();
            Soldier = UnityGameFramework.Runtime.GameEntry.GetComponent<SoldierComponent>();
            if (Soldier)
                Log.Info("获取Soldier组件成功");
            else
                Log.Info("获取Soldier组件失败");
            Warlord = UnityGameFramework.Runtime.GameEntry.GetComponent<WarlordComponent>();
            if (Warlord)
                Log.Info("获取Warlord组件成功");
            else
                Log.Info("获取Warlord组件失败");
        }
    }
}
