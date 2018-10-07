using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameFramework;
using UnityGameFramework.Runtime;
using System;

namespace RoyalWar
{
    public class MainForm : UGuiForm
    {
        public Text m_Timer;
        public Transform SoldierItemInstanceRoot;
        public Transform WarlordItemInstanceRoot;
        private ProcedureMain m_procedureMain = null;
        private int timer = 180;
#if UNITY_2017_3_OR_NEWER
        protected override void OnOpen(object userData)
#else
        protected internal override void OnOpen(object userData)
#endif
        {
            base.OnOpen(userData);

            m_procedureMain = (ProcedureMain)userData;
            if (m_procedureMain == null)
            {
                Log.Warning("ProcedureMenu is invalid when open MenuForm.");
                return;
            }

            InvokeRepeating("Timer", 1.0f, 1.0f);
            //LoadSoldierSpirit();
            //m_QuitButton.SetActive(Application.platform != RuntimePlatform.IPhonePlayer);
        }

        private void Timer()
        {
            timer--;
            if (timer < 0)
            {
                CancelInvoke();
                return;
            }
            TimeSpan ts = new TimeSpan(0, 0, timer);
            m_Timer.text = ts.Minutes+":"+ts.Seconds;
        }

        public void LoadSoldierSpirit()
        {
            int num = 6;
            SoldierComponent soldierComponent = GameEntry.Soldier;
            if (soldierComponent == null)
            {
                Log.Info("soldier组件为空");
            }
            soldierComponent.SoldierItemInstanceRoot = this.SoldierItemInstanceRoot;

            for (int i = 0; i < num; i++)
            {
                soldierComponent.ShowSoldierItem((SoldierType)i);
            }
        }

        public void LoadWarlordSpirit()
        {
            int num = 2;
            WarlordComponent warlordComponent = GameEntry.Warlord;
            warlordComponent.WarlordItemInstanceRoot = this.WarlordItemInstanceRoot;

            for(int i = 0; i< num; i++)
            {
                warlordComponent.ShowWarlordItem();
            }
        }
    }
}
