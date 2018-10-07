using GameFramework;
using GameFramework.ObjectPool;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace RoyalWar
{
    public class WarlordComponent : GameFrameworkComponent
    {
        [SerializeField]
        private WarlordItem m_WarlordItemTemplate;

        [SerializeField]
        private int m_InstancePoolCapacity = 6;
        
        private Transform m_WarlordItemInstanceRoot;
        private IObjectPool<WarlordItemObject> m_WarlordItemObjectPool;
        private List<WarlordItem> m_SpiritList;

        // Use this for initialization
        void Start()
        {
            Log.Info(m_InstancePoolCapacity);
            m_WarlordItemObjectPool = GameEntry.ObjectPool.CreateSingleSpawnObjectPool<WarlordItemObject>("WarlordItem", m_InstancePoolCapacity);
            m_SpiritList = new List<WarlordItem>();
        }

        private void OnDestroy()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public Transform WarlordItemInstanceRoot
        {
            get { return m_WarlordItemInstanceRoot; }
            set { m_WarlordItemInstanceRoot = value; }
        }


        public void ShowWarlordItem()
        {
            WarlordItem warlordItem = CreateWarlordItem();
            if (warlordItem != null)
            {
                warlordItem.Init();
                m_SpiritList.Add(warlordItem);
            }
        }

        public void DestroySoldierItem()
        {
            foreach(var item in m_SpiritList)
            {
                UnityEngine.Object.Destroy(item.gameObject);
            }
            m_SpiritList.Clear();
        }

        private void HideWarlordItem(WarlordItem hpBarItem)
        {
            //SoldierItem.Reset();
            m_SpiritList.Remove(hpBarItem);
            m_WarlordItemObjectPool.Unspawn(hpBarItem);
        }

        public WarlordItem CreateWarlordItem()
        {
            WarlordItem warlordItem = null;
            WarlordItemObject warlordItemItemObject = m_WarlordItemObjectPool.Spawn();
            if (warlordItemItemObject != null)
            {
                warlordItem = (WarlordItem)warlordItemItemObject.Target;
            }
            else
            {
                warlordItem = Instantiate(m_WarlordItemTemplate);
                Transform transform = warlordItem.GetComponent<Transform>();
                transform.SetParent(m_WarlordItemInstanceRoot);
                transform.localScale = Vector3.one;
                m_WarlordItemObjectPool.Register(new WarlordItemObject(warlordItem), true);
            }

            return warlordItem;
        }
    } 
}
