using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework.DataTable;
using System;

namespace RoyalWar
{
    public class DREntity : IDataRow
    {

        public int Id
        {
            get;protected set;
        }

        public string AssetName
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
        }

        private void AvoidJIT()
        {
            new Dictionary<int, DREntity>();
        }
    } 
}
