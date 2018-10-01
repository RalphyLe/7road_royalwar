
using System;
using GameFramework;
using GameFramework.Event;
using GameFramework.Resource;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

namespace RoyalWar
{
    public class ProcedurePreload : ProcedureBase
    {
        private Dictionary<string, bool> _loaded_flag = new Dictionary<string, bool>();
        public override bool UseNativeDialog
        {
            get
            {
                return true;
            }
        }

        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            Log.Debug("进入ProcedurePreLoad流程");
            base.OnEnter(procedureOwner);

            GameEntry.Event.Subscribe(LoadConfigSuccessEventArgs.EventId, OnLoadConfigSuccess);
            GameEntry.Event.Subscribe(LoadConfigFailureEventArgs.EventId, OnLoadConfigFailure);
            GameEntry.Event.Subscribe(LoadDataTableSuccessEventArgs.EventId, OnLoadDataTableSuccess);
            GameEntry.Event.Subscribe(LoadDataTableFailureEventArgs.EventId, OnLoadDataTableFailure);

            _loaded_flag.Clear();

            PreLoadResources();
        }

        protected override void OnLeave(ProcedureOwner procedureOwner,bool isShutdown)
        {
            GameEntry.Event.Unsubscribe(LoadDataTableSuccessEventArgs.EventId, OnLoadDataTableSuccess);
            GameEntry.Event.Unsubscribe(LoadDataTableFailureEventArgs.EventId, OnLoadDataTableFailure);

            base.OnLeave(procedureOwner, isShutdown);
        }

        protected override void OnUpdate(ProcedureOwner procedureOwner,float elapseSeconds,float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
            IEnumerator<bool> iter = _loaded_flag.Values.GetEnumerator();
            while (iter.MoveNext())
            {
                if (!iter.Current)
                {
                    return;
                }
            }

            procedureOwner.SetData<VarInt>(Constant.ProcedureData.NextSceneId, GameEntry.Config.GetInt("Scene.Menu"));
            ChangeState<ProcedureChangeScene>(procedureOwner);
        }

        private void PreLoadResources()
        {
            //Preload configs
            LoadConfig("DefaultConfig");
            Log.Info(AssetUtility.GetConfigAsset("DefaultConfig"));

            //Preload UI
            LoadDataTable("UIForm");
            Log.Info(AssetUtility.GetDataTableAsset("UIForm"));
        }

        private void LoadDataTable(string v)
        {
            _loaded_flag.Add(string.Format("Config.{0}", v), false);
            GameEntry.Config.LoadConfig(v, this);
        }

        private void LoadConfig(string v)
        {
            _loaded_flag.Add(string.Format("DataTable.{0}", v), false);
            GameEntry.DataTable.LoadDataTable(v, this);
        }

        private void OnLoadConfigFailure(object sender, GameEventArgs e)
        {
            LoadConfigFailureEventArgs ne = (LoadConfigFailureEventArgs)e;
            if(ne.UserData != this)
            {
                return;
            }

            Log.Error("Can not load config '{0}' from '{1}' with error message '{2}' ", ne.ConfigName, ne.ConfigAssetName, ne.ErrorMessage);
        }

        private void OnLoadConfigSuccess(object sender, GameEventArgs e)
        {
            LoadConfigSuccessEventArgs ne = (LoadConfigSuccessEventArgs)e;
            if(ne.UserData != this)
            {
                return;
            }

            _loaded_flag[string.Format("Config.{0}", ne.ConfigName)] = true;
            Log.Info("Load config '{0}' OK.", ne.ConfigName);
        }
        private void OnLoadDataTableFailure(object sender, GameEventArgs e)
        {
            LoadDataTableFailureEventArgs ne = (LoadDataTableFailureEventArgs)e;
            if(ne.UserData != this)
            {
                return;
            }
            Log.Error("Can not load data table '{0}' from '{1}' with error message '{2}'", ne.DataTableName, ne.DataTableAssetName, ne.ErrorMessage);
        }

        private void OnLoadDataTableSuccess(object sender, GameEventArgs e)
        {
            LoadDataTableSuccessEventArgs ne = (LoadDataTableSuccessEventArgs)e;
            if (ne.UserData != this)
            {
                return;
            }
            _loaded_flag[string.Format("DataTable.{0}", ne.DataTableAssetName)] = true;
            Log.Info("Load data table '{0}' OK.", ne.DataTableAssetName);
        }
    }
}