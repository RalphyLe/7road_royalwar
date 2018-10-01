//------------------------------------------------------------
// Game Framework v3.x
// Copyright © 2013-2018 Jiang Yin. All rights reserved.
// Homepage: http://gameframework.cn/
// Feedback: mailto:jiangyin@gameframework.cn
//------------------------------------------------------------

using GameFramework;
using GameFramework.DataNode;
using UnityEngine;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 数据结点组件。
    /// </summary>
    [DisallowMultipleComponent]
    [AddComponentMenu("Game Framework/Data Node")]
    public sealed class DataNodeComponent : GameFrameworkComponent
    {
        private IDataNodeManager m_DataNodeManager = null;

        /// <summary>
        /// 游戏框架组件初始化。
        /// </summary>
        protected override void Awake()
        {
            base.Awake();

            m_DataNodeManager = GameFrameworkEntry.GetModule<IDataNodeManager>();
            if (m_DataNodeManager == null)
            {
                Log.Fatal("Data node manager is invalid.");
                return;
            }
        }

        private void Start()
        {
            DataNodeComponent dataNodeComponent = GameEntry.GetComponent<DataNodeComponent>();

            // 使用数据结点组件，直接通过绝对路径获取或设置数据
            dataNodeComponent.SetData<VarString>("Player.Name", "Ellan");
            string playerName = dataNodeComponent.GetData<VarString>("Player.Name");
            Log.Info("Player name is {0}.", playerName);

            // 使用数据结点组件，通过参照某个数据结点和相对路径获取或设置数据
            IDataNode playerNode = dataNodeComponent.GetNode("Player");
            dataNodeComponent.SetData<VarInt>("Level", 99, playerNode);
            int playerLevel = dataNodeComponent.GetData<VarInt>("Level", playerNode);
            Log.Info("Player level is {0}.", playerLevel.ToString());

            // 使用数据结点组件获取数据结点后，通过数据结点的接口进行更多操作。
            IDataNode playerExpNode = playerNode.GetOrAddChild("Exp");
            playerExpNode.SetData<VarInt>(1000);
            int playerExp = playerExpNode.GetData<VarInt>();
            Log.Info("Player exp is {0}.", playerExp.ToString());
        }

        /// <summary>
        /// 获取根数据结点。
        /// </summary>
        public IDataNode Root
        {
            get
            {
                return m_DataNodeManager.Root;
            }
        }

        /// <summary>
        /// 根据类型获取数据结点的数据。
        /// </summary>
        /// <typeparam name="T">要获取的数据类型。</typeparam>
        /// <param name="path">相对于 node 的查找路径。</param>
        /// <returns>指定类型的数据。</returns>
        public T GetData<T>(string path) where T : Variable
        {
            return m_DataNodeManager.GetData<T>(path);
        }

        /// <summary>
        /// 获取数据结点的数据。
        /// </summary>
        /// <param name="path">相对于 node 的查找路径。</param>
        /// <returns>数据结点的数据。</returns>
        public Variable GetData(string path)
        {
            return m_DataNodeManager.GetData(path);
        }

        /// <summary>
        /// 根据类型获取数据结点的数据。
        /// </summary>
        /// <typeparam name="T">要获取的数据类型。</typeparam>
        /// <param name="path">相对于 node 的查找路径。</param>
        /// <param name="node">查找起始结点。</param>
        /// <returns>指定类型的数据。</returns>
        public T GetData<T>(string path, IDataNode node) where T : Variable
        {
            return m_DataNodeManager.GetData<T>(path, node);
        }

        /// <summary>
        /// 获取数据结点的数据。
        /// </summary>
        /// <param name="path">相对于 node 的查找路径。</param>
        /// <param name="node">查找起始结点。</param>
        /// <returns>数据结点的数据。</returns>
        public Variable GetData(string path, IDataNode node)
        {
            return m_DataNodeManager.GetData(path, node);
        }

        /// <summary>
        /// 设置数据结点的数据。
        /// </summary>
        /// <typeparam name="T">要设置的数据类型。</typeparam>
        /// <param name="path">相对于 node 的查找路径。</param>
        /// <param name="data">要设置的数据。</param>
        public void SetData<T>(string path, T data) where T : Variable
        {
            m_DataNodeManager.SetData(path, data);
        }

        /// <summary>
        /// 设置数据结点的数据。
        /// </summary>
        /// <param name="path">相对于 node 的查找路径。</param>
        /// <param name="data">要设置的数据。</param>
        public void SetData(string path, Variable data)
        {
            m_DataNodeManager.SetData(path, data);
        }

        /// <summary>
        /// 设置数据结点的数据。
        /// </summary>
        /// <typeparam name="T">要设置的数据类型。</typeparam>
        /// <param name="path">相对于 node 的查找路径。</param>
        /// <param name="data">要设置的数据。</param>
        /// <param name="node">查找起始结点。</param>
        public void SetData<T>(string path, T data, IDataNode node) where T : Variable
        {
            m_DataNodeManager.SetData(path, data, node);
        }

        /// <summary>
        /// 设置数据结点的数据。
        /// </summary>
        /// <param name="path">相对于 node 的查找路径。</param>
        /// <param name="data">要设置的数据。</param>
        /// <param name="node">查找起始结点。</param>
        public void SetData(string path, Variable data, IDataNode node)
        {
            m_DataNodeManager.SetData(path, data, node);
        }

        /// <summary>
        /// 获取数据结点。
        /// </summary>
        /// <param name="path">相对于 node 的查找路径。</param>
        /// <returns>指定位置的数据结点，如果没有找到，则返回空。</returns>
        public IDataNode GetNode(string path)
        {
            return m_DataNodeManager.GetNode(path);
        }

        /// <summary>
        /// 获取数据结点。
        /// </summary>
        /// <param name="path">相对于 node 的查找路径。</param>
        /// <param name="node">查找起始结点。</param>
        /// <returns>指定位置的数据结点，如果没有找到，则返回空。</returns>
        public IDataNode GetNode(string path, IDataNode node)
        {
            return m_DataNodeManager.GetNode(path, node);
        }

        /// <summary>
        /// 获取或增加数据结点。
        /// </summary>
        /// <param name="path">相对于 node 的查找路径。</param>
        /// <returns>指定位置的数据结点，如果没有找到，则增加相应的数据结点。</returns>
        public IDataNode GetOrAddNode(string path)
        {
            return m_DataNodeManager.GetOrAddNode(path);
        }

        /// <summary>
        /// 获取或增加数据结点。
        /// </summary>
        /// <param name="path">相对于 node 的查找路径。</param>
        /// <param name="node">查找起始结点。</param>
        /// <returns>指定位置的数据结点，如果没有找到，则增加相应的数据结点。</returns>
        public IDataNode GetOrAddNode(string path, IDataNode node)
        {
            return m_DataNodeManager.GetOrAddNode(path, node);
        }

        /// <summary>
        /// 移除数据结点。
        /// </summary>
        /// <param name="path">相对于 node 的查找路径。</param>
        public void RemoveNode(string path)
        {
            m_DataNodeManager.RemoveNode(path);
        }

        /// <summary>
        /// 移除数据结点。
        /// </summary>
        /// <param name="path">相对于 node 的查找路径。</param>
        /// <param name="node">查找起始结点。</param>
        public void RemoveNode(string path, IDataNode node)
        {
            m_DataNodeManager.RemoveNode(path, node);
        }

        /// <summary>
        /// 移除所有数据结点。
        /// </summary>
        public void Clear()
        {
            m_DataNodeManager.Clear();
        }
    }
}
