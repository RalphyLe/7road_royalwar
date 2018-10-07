﻿using System.Collections.Generic;
using GameFramework;
using GameFramework.Event;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

namespace RoyalWar
{
    internal class ProcedureMain : ProcedureBase
    {
        private MainForm m_MainForm = null;
        private UnityEngine.Camera camera;

        private UnityEngine.Vector3 plane_normal;
        private UnityEngine.Vector3 inner_point;
        private UnityEngine.Plane plane;

        private UnityEngine.Rect drag_area;
        private UnityEngine.Vector3 down = UnityEngine.Vector3.zero;
        private UnityEngine.Vector3 move = UnityEngine.Vector3.zero;

        private readonly Dictionary<GameMode, GameBase> m_Games = new Dictionary<GameMode, GameBase>();
        private GameBase m_CurrentGame = null;

        public override bool UseNativeDialog
        {
            get
            {
                return false;
            }
        }

        protected override void OnInit(ProcedureOwner procedureOwner)
        {
            base.OnInit(procedureOwner);

            m_Games.Add(GameMode.Survival, new SurvivalGame());
        }

        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            Log.Debug("进入ProcedureMenu流程");
            base.OnEnter(procedureOwner);
            plane_normal = new UnityEngine.Vector3(0,0,-1);
            inner_point = new UnityEngine.Vector3(4.76f, 1.2f, 0);
            plane = new UnityEngine.Plane(plane_normal, inner_point);

            GameEntry.Event.Subscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);
            GameEntry.UI.OpenUIForm(UIFormId.MainForm, this);

            GameMode gameMode = (GameMode)procedureOwner.GetData<VarInt>(Constant.ProcedureData.GameMode).Value;
            m_CurrentGame = m_Games[gameMode];
            m_CurrentGame.Initialize();
        }

        protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);

            GameEntry.Event.Unsubscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);

            if (m_MainForm != null)
            {
                m_MainForm.Close(isShutdown);
                m_MainForm = null;
            }
            SoldierComponent soldierComponent = GameEntry.Soldier;
            soldierComponent.DestroySoldierItem();
        }

        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            DragCamera();

            if (m_CurrentGame != null && !m_CurrentGame.GameOver)
            {
                m_CurrentGame.Update(elapseSeconds, realElapseSeconds);
                return;
            }
        }

        private void OnOpenUIFormSuccess(object sender, GameEventArgs e)
        {
            OpenUIFormSuccessEventArgs ne = (OpenUIFormSuccessEventArgs)e;
            if (ne.UserData != this)
            {
                return;
            }

            m_MainForm = (MainForm)ne.UIForm.Logic;
            drag_area = new UnityEngine.Rect(new UnityEngine.Vector2(0,140), new UnityEngine.Vector2(1280,520));
            Log.Info("drag_area init success! width={0},height={1}", drag_area.width, drag_area.height);
            m_MainForm.LoadSoldierSpirit();
            m_MainForm.LoadWarlordSpirit();
            //LoadSoldierSpirit();
        }
        protected override void OnDestroy(ProcedureOwner procedureOwner)
        {
            base.OnDestroy(procedureOwner);

            m_Games.Clear();
        }

        private void DragCamera()
        {
            if (!camera)
                camera = UnityEngine.Camera.main;
            float offset = 0;
            if (UnityEngine.Input.GetMouseButtonDown(0) && drag_area.Contains(UnityEngine.Input.mousePosition))
            {
                down = UnityEngine.Input.mousePosition;
                Log.Info("Mouse Position {0},{1}", down.x, down.y);
                move = UnityEngine.Input.mousePosition;
            }
            if (UnityEngine.Input.GetMouseButton(0) && drag_area.Contains(down))
            {
                move = UnityEngine.Input.mousePosition;
                offset = (move.x - down.x)/100;
                if (UnityEngine.Mathf.Abs(offset) > 0.1)
                {
                    UnityEngine.Vector3 pos = camera.transform.position;
                    pos.x -= offset;
                    pos.x = UnityEngine.Mathf.Clamp(pos.x, -1, 11);
                    camera.transform.position = pos;
                    down = move; 
                }
            }
            if(UnityEngine.Input.GetMouseButtonUp(0) && drag_area.Contains(down))
            {
                down = UnityEngine.Vector3.zero;
                move = UnityEngine.Vector3.zero;
            }
        }
    }
}