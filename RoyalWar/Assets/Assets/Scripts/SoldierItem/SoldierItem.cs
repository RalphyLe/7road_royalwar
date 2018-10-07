using GameFramework;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace RoyalWar
{
    public enum SoldierType
    {
        Infantry = 0,//步兵
        Pikeman,//枪兵
        Cavalry,//骑兵
        Dragon,//骑兵
        Enchanter,//魔法师
        Titan,//泰坦巨人
        Warlord//佣兵
    }

    public class SoldierItem : MonoBehaviour
    {
        /// <summary>
        /// 士兵类型
        /// </summary>
        public SoldierType Type
        {
            get;
            set;
        }
        

        public void Init(SoldierType type)
        {
            Type = type;
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnPointerDown()
        {
            Log.Info("PointEnter");
            
        }

        public void OnPointExit()
        {

        }

        public void OnPointUp()
        {

        }
    } 
}
