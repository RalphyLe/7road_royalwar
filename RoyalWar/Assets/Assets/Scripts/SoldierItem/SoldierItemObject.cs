using GameFramework.ObjectPool;
using UnityEngine;

namespace RoyalWar
{
    public class SoldierItemObject : ObjectBase
    {
        public SoldierItemObject(object target) : base(target)
        {

        }

        protected override void Release(bool isShutdown)
        {
            SoldierItem soldierItem = (SoldierItem)Target;
            if(soldierItem == null)
            {
                return;
            }

            Object.Destroy(soldierItem.gameObject);
        }
    } 
}
