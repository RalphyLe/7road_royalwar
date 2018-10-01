
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
            throw new NotImplementedException();
        }

        private void OnLoadDataTableFailure(object sender, GameEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnLoadDataTableSuccess(object sender, GameEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}