using System;
using GameFramework;
using GameFramework.Event;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

namespace RoyalWar
{
    public class ProcedureMenu : ProcedureBase
    {
        public bool m_StartGame = false;
        private EnterForm m_EnterForm = null;

        public override bool UseNativeDialog
        {
            get
            {
                return false;
            }
        }

        public void StartGame()
        {
            m_StartGame = true;
        }

        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            Log.Debug("进入ProcedureMenu流程");
            base.OnEnter(procedureOwner);

            GameEntry.Event.Subscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);

            m_StartGame = false;
            GameEntry.UI.OpenUIForm(UIFormId.EnterForm, this);
        }
        protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);

            GameEntry.Event.Unsubscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);

            if (m_EnterForm != null)
            {
                m_EnterForm.Close(isShutdown);
                m_EnterForm = null;
            }
        }

        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

            if (m_StartGame)
            {
                procedureOwner.SetData<VarInt>(Constant.ProcedureData.NextSceneId, GameEntry.Config.GetInt("Scene.Main"));
                //procedureOwner.SetData<VarInt>(Constant.ProcedureData.GameMode, (int)GameMode.Survival);
                ChangeState<ProcedureChangeScene>(procedureOwner);
            }
        }

        private void OnOpenUIFormSuccess(object sender, GameEventArgs e)
        {
            OpenUIFormSuccessEventArgs ne = (OpenUIFormSuccessEventArgs)e;
            if (ne.UserData != this)
            {
                return;
            }

            m_EnterForm = (EnterForm)ne.UIForm.Logic;
        }
    }
}