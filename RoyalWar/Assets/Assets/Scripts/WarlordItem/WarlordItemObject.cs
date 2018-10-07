using GameFramework.ObjectPool;
using UnityEngine;

namespace RoyalWar
{
    public class WarlordItemObject : ObjectBase
    {
        public WarlordItemObject(object target) : base(target)
        {

        }

        protected override void Release(bool isShutdown)
        {
            WarlordItem warlordItem = (WarlordItem)Target;
            if(warlordItem == null)
            {
                return;
            }

            Object.Destroy(warlordItem.gameObject);
        }
    } 
}
