using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameFramework;
using UnityGameFramework.Runtime;

namespace  RoyalWar
{
    public class EnterForm : UGuiForm
    {
        [SerializeField]
        public GameObject m_MenuPanel = null;
        public Image m_ProgressBar = null;
        public Text m_Progress = null;

        private ProcedureMenu m_procedureMenu = null;

        public void OnStartButtonClick()
        {

        }

        public void OnSettingButtonClick()
        {

        }

        public void OnQuitButtonClick()
        {

        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnOpen(object userData)
#else
        protected internal override void OnOpen(object userData)
#endif
        {
            base.OnOpen(userData);

            m_procedureMenu = (ProcedureMenu)userData;
            if (m_procedureMenu == null)
            {
                Log.Warning("ProcedureMenu is invalid when open MenuForm.");
                return;
            }

            //m_QuitButton.SetActive(Application.platform != RuntimePlatform.IPhonePlayer);
        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnClose(object userData)
#else
        protected internal override void OnClose(object userData)
#endif
        {
            m_procedureMenu = null;

            base.OnClose(userData);
        }
    } 
}
