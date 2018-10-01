using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework.DataTable;
using System;

namespace RoyalWar
{
    /// <summary>
    /// 场配置表
    /// </summary>
    public class DRScene : IDataRow
    {
        /// <summary>
        /// 资源名称
        /// </summary>
        public string AssetName { get; protected set; }
        /// <summary>
        /// 背景音乐编号
        /// </summary>
        public int BackgroundMusicId { get; protected set; }
        /// <summary>
        /// 场景编号
        /// </summary>
        public int Id
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
            BackgroundMusicId = int.Parse(text[index++]);
        }

        private void AvoidJIT()
        {
            new Dictionary<int, DRScene>();
        }
    }
}