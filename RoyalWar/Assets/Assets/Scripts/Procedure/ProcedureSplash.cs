using System;
using GameFramework;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

namespace RoyalWar
{
    public class ProcedureSplash : ProcedureBase
    {
        public override bool UseNativeDialog
        {
            get
            {
                return true;
            }
        }

        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);
            //Log.Debug("进入ProceSplash流程");
        }

        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
            //TODO:增加一个splash动画，这里先跳过
            //编辑器模式下，直接进入预加载流程，否则检测一下版本
            ChangeState<ProcedurePreload>(procedureOwner);
        }
    }
}