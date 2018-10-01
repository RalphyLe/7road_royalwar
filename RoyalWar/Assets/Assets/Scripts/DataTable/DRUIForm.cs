using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework.DataTable;
using System;

namespace RoyalWar
{
    /// <summary>
    /// 界面配置表
    /// </summary>
    public class DRUIForm : IDataRow
    {
        /// <summary>
        /// 界面编号
        /// </summary>
        public int Id
        {
            get;
            protected set;
        }

        /// <summary>
        /// 资源名称
        /// </summary>
        public string AssetName
        {
            get;
            protected set;
        }
        /// <summary>
        /// 界面组名称
        /// </summary>
        public string UIGroupName
        {
            get;
            protected set;
        }

        /// <summary>
        /// 是否允许多个界面实例
        /// </summary>
        public bool AllowMultiInstance
        {
            get;
            protected set;
        }

        /// <summary>
        /// 是否暂停被其覆盖的界面
        /// </summary>
        public bool PauseCoveredUIForm
        {
            get;
            protected set;
        }

        public void ParseDataRow(string dataRowText)
        {
            string[] text = DataTableExtension.SplitDataRow(dataRowText);
            int index = 0;
            index++;
            Id = int.Parse(text[index++]);
            index++;
            AssetName = text[index++];
            UIGroupName = text[index++];
            AllowMultiInstance = bool.Parse(text[index++]);
            PauseCoveredUIForm = bool.Parse(text[index++]);
        }

        private void AvoidJIT()
        {
            new Dictionary<int, DRUIForm>();
        }
    } 
}
