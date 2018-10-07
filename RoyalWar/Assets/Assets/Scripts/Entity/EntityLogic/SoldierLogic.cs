using GameFramework;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace RoyalWar
{
    public class SoldierLogic : Entity
    {
        [SerializeField]
        private SoldierData m_SoldierSpiritData = null;

#if UNITY_2017_3_OR_NEWER
        protected override void OnShow(object userData)
#else
        protected internal override void OnShow(object userData)
#endif
        {
            base.OnShow(userData);
            m_SoldierSpiritData = userData as SoldierData;

            Log.Info("加载战士Spirit完成");
        }
    }
}
