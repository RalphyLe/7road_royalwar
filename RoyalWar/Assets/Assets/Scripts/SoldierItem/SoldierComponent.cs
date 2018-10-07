using GameFramework;
using GameFramework.ObjectPool;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace RoyalWar
{
    public class SoldierComponent : GameFrameworkComponent
    {
        [SerializeField]
        private SoldierItem m_SoldierItemTemplate;

        [SerializeField]
        private int m_InstancePoolCapacity = 6;

        private Transform m_SoldierItemInstanceRoot;
        private IObjectPool<SoldierItemObject> m_SoldierItemObjectPool;
        private List<SoldierItem> m_SpiritList;

        // Use this for initialization
        void Start()
        {
            //if (m_SoldierItemInstanceRoot == null)
            //{
            //    Log.Error("You must set HP bar instance root first.");
            //    return;
            //}
            Log.Info(m_InstancePoolCapacity);
            m_SoldierItemObjectPool = GameEntry.ObjectPool.CreateSingleSpawnObjectPool<SoldierItemObject>("SoldierItem", m_InstancePoolCapacity);
            m_SpiritList = new List<SoldierItem>();
        }

        private void OnDestroy()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public Transform SoldierItemInstanceRoot
        {
            get { return m_SoldierItemInstanceRoot; }
            set { m_SoldierItemInstanceRoot = value; }
        }


        public void ShowSoldierItem(SoldierType type)
        {
            SoldierItem soldierItem = CreateSoldierItem();
            if (soldierItem != null)
            {
                soldierItem.Init(type);
                m_SpiritList.Add(soldierItem);
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

        private void HideSoldierItem(SoldierItem hpBarItem)
        {
            //SoldierItem.Reset();
            m_SpiritList.Remove(hpBarItem);
            m_SoldierItemObjectPool.Unspawn(hpBarItem);
        }

        public SoldierItem CreateSoldierItem()
        {
            SoldierItem soldierItem = null;
            SoldierItemObject soldierItemItemObject = m_SoldierItemObjectPool.Spawn();
            if (soldierItemItemObject != null)
            {
                soldierItem = (SoldierItem)soldierItemItemObject.Target;
            }
            else
            {
                soldierItem = Instantiate(m_SoldierItemTemplate);
                Transform transform = soldierItem.GetComponent<Transform>();
                transform.SetParent(m_SoldierItemInstanceRoot);
                transform.localScale = Vector3.one;
                m_SoldierItemObjectPool.Register(new SoldierItemObject(soldierItem), true);
            }

            return soldierItem;
        }
    } 
}
