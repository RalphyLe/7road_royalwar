﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using GameFramework;
using UnityGameFramework.Runtime;

namespace RoyalWar
{
    public static class DataTableExtension
    {

        private const string DataRowClassPrefixName = "RoyalWar.DR";
        private static readonly string[] ColumnSplit = new string[] { "\t" };
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataTableComponent"></param>
        /// <param name="dataTableName"></param>
        /// <param name="userData"></param>
        public static void LoadDataTable(this DataTableComponent dataTableComponent,string dataTableName,object userData = null)
        {
            if (string.IsNullOrEmpty(dataTableName))
            {
                Log.Warning("Data table name is invalid.");
                return;
            }

            string[] splitNames = dataTableName.Split('_');
            if (splitNames.Length > 2)
            {
                Log.Warning("Data table name is invalid");
                return;
            }

            string dataRowClassName = DataRowClassPrefixName + splitNames[0];
            Type dataRowType = Type.GetType(dataRowClassName);
            if(dataRowType == null)
            {
                Log.Warning("Can not get data row type with class name '{0}'.", dataRowClassName);
                return;
            }

            string dataTableNameInType = splitNames.Length>1?splitNames[1]:null;
            dataTableComponent.LoadDataTable(dataRowType, dataTableName, dataTableNameInType, AssetUtility.GetDataTableAsset(dataTableName), Constant.AssetPriority.DataTableAsset, userData);
        }

        public static string[] SplitDataRow(string dataRowText)
        {
            return dataRowText.Split(ColumnSplit, StringSplitOptions.None);
        }
    } 
}
