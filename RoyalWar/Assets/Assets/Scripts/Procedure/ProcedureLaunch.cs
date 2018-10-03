using System;
using System.Collections.Generic;
using GameFramework;
using GameFramework.Localization;
using UnityEngine;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

namespace RoyalWar
{
    /// <summary>
    /// 游戏初始流程
    /// </summary>
    class ProcedureLaunch : ProcedureBase
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
            Log.Debug("进入ProcedureLaunch流程");
            base.OnEnter(procedureOwner);

            // 语言配置：设置当前使用的语言，如果不设置，则默认使用操作系统语言。
            InitLanguageSettings();

        }

        protected override void OnUpdate(ProcedureOwner procedureOwner,float elapseSeconds,float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
            ChangeState<ProcedureSplash>(procedureOwner);
        }

        private void InitLanguageSettings()
        {
            if (GameEntry.Base.EditorResourceMode && GameEntry.Base.EditorLanguage != Language.Unspecified)
            {
                // 编辑器资源模式直接使用 Inspector 上设置的语言
                return;
            }

            Language language = GameEntry.Localization.Language;
            string languageString = GameEntry.Setting.GetString(Constant.Setting.Language);
            if (!string.IsNullOrEmpty(languageString))
            {
                try
                {
                    language = (Language)Enum.Parse(typeof(Language), languageString);
                }
                catch
                {

                }
            }

            if (language != Language.English
                && language != Language.ChineseSimplified
                && language != Language.ChineseTraditional
                && language != Language.Korean)
            {
                // 若是暂不支持的语言，则使用英语
                language = Language.English;

                GameEntry.Setting.SetString(Constant.Setting.Language, language.ToString());
                GameEntry.Setting.Save();
            }

            GameEntry.Localization.Language = language;

            Log.Info("Init language settings complete, current language is '{0}'.", language.ToString());
        }
    }
}
