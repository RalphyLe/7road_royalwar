
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
            GameEntry.Event.Subscribe(LoadDictionarySuccessEventArgs.EventId, OnLoadDictionarySuccess);
            GameEntry.Event.Subscribe(LoadDictionaryFailureEventArgs.EventId, OnLoadDictionaryFailure);

            _loaded_flag.Clear();

            PreLoadResources();
        }

        protected override void OnLeave(ProcedureOwner procedureOwner,bool isShutdown)
        {
            GameEntry.Event.Unsubscribe(LoadConfigSuccessEventArgs.EventId, OnLoadConfigSuccess);
            GameEntry.Event.Unsubscribe(LoadConfigFailureEventArgs.EventId, OnLoadConfigFailure);
            GameEntry.Event.Unsubscribe(LoadDataTableSuccessEventArgs.EventId, OnLoadDataTableSuccess);
            GameEntry.Event.Unsubscribe(LoadDataTableFailureEventArgs.EventId, OnLoadDataTableFailure);
            GameEntry.Event.Unsubscribe(LoadDictionarySuccessEventArgs.EventId, OnLoadDictionarySuccess);
            GameEntry.Event.Unsubscribe(LoadDictionaryFailureEventArgs.EventId, OnLoadDictionaryFailure);

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
            //var config = GameEntry.Config;
            //Log.Info("Scene.Enter {0}", GameEntry.Config.GetInt("Scene.Enter"));
            procedureOwner.SetData<VarInt>(Constant.ProcedureData.NextSceneId, GameEntry.Config.GetInt("Scene.Enter"));
            ChangeState<ProcedureChangeScene>(procedureOwner);
        }

        private void PreLoadResources()
        {
            //Preload configs
            LoadConfig("DefaultConfig");
            Log.Info(AssetUtility.GetConfigAsset("DefaultConfig"));

            //PreLoad dataTable
            LoadDataTable("Scene");
            Log.Info(AssetUtility.GetDataTableAsset("Scene"));
            LoadDataTable("UIForm");
            Log.Info(AssetUtility.GetDataTableAsset("UIForm"));
            LoadDataTable("Entity");
            Log.Info(AssetUtility.GetDataTableAsset("Entity"));

            // Preload dictionaries
            LoadDictionary("Default");
            Log.Info(AssetUtility.GetDictionaryAsset("Default"));
            // Preload fonts
            LoadFont("MainFont");
            Log.Info(AssetUtility.GetFontAsset("MainFont"));
        }

        private void LoadDataTable(string v)
        {
            _loaded_flag.Add(string.Format("DataTable.{0}", v), false);
            GameEntry.DataTable.LoadDataTable(v, this);
        }

        private void LoadConfig(string v)
        {
            _loaded_flag.Add(string.Format("Config.{0}", v), false);
            GameEntry.Config.LoadConfig(v, this);
        }

        private void LoadDictionary(string v)
        {
            _loaded_flag.Add(string.Format("Dictionary.{0}", v), false);
            GameEntry.Localization.LoadDictionary(v, this);
        }

        private void LoadFont(string fontName)
        {
            _loaded_flag.Add(string.Format("Font.{0}", fontName), false);
            GameEntry.Resource.LoadAsset(AssetUtility.GetFontAsset(fontName), Constant.AssetPriority.FontAsset, new LoadAssetCallbacks(
                (assetName, asset, duration, userData) =>
                {
                    _loaded_flag[string.Format("Font.{0}", fontName)] = true;
                    UGuiForm.SetMainFont((Font)asset);
                    Log.Info("Load font '{0}' OK.", fontName);
                },

                (assetName, status, errorMessage, userData) =>
                {
                    Log.Error("Can not load font '{0}' from '{1}' with error message '{2}'.", fontName, assetName, errorMessage);
                }));
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
            var config = GameEntry.Config;
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
            _loaded_flag[string.Format("DataTable.{0}", ne.DataTableName)] = true;
            Log.Info("Load data table '{0}' OK.", ne.DataTableName);
        }

        private void OnLoadDictionarySuccess(object sender, GameEventArgs e)
        {
            LoadDictionarySuccessEventArgs ne = (LoadDictionarySuccessEventArgs)e;
            if (ne.UserData != this)
            {
                return;
            }

            _loaded_flag[string.Format("Dictionary.{0}", ne.DictionaryName)] = true;
            Log.Info("Load dictionary '{0}' OK.", ne.DictionaryName);
        }

        private void OnLoadDictionaryFailure(object sender, GameEventArgs e)
        {
            LoadDictionaryFailureEventArgs ne = (LoadDictionaryFailureEventArgs)e;
            if (ne.UserData != this)
            {
                return;
            }

            Log.Error("Can not load dictionary '{0}' from '{1}' with error message '{2}'.", ne.DictionaryName, ne.DictionaryAssetName, ne.ErrorMessage);
        }
    }
}