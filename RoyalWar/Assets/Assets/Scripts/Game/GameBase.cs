using System;
using GameFramework;
using GameFramework.Event;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace RoyalWar
{
    public abstract class GameBase
    {
        public abstract GameMode GameMode
        {
            get;
        }

        public bool GameOver
        {
            get;
            protected set;
        }

        public virtual void Initialize()
        {
            GameEntry.Event.Subscribe(ShowEntitySuccessEventArgs.EventId, OnShowEntitySuccess);
            GameEntry.Event.Subscribe(ShowEntityFailureEventArgs.EventId, OnShowEntityFailure);

            GameOver = false;
        }

        public virtual void Shutdown()
        {
            GameEntry.Event.Unsubscribe(ShowEntitySuccessEventArgs.EventId, OnShowEntitySuccess);
            GameEntry.Event.Unsubscribe(ShowEntityFailureEventArgs.EventId, OnShowEntityFailure);
        }

        public virtual void Update(float elapseSeconds, float realElapseSeconds)
        {
        }

        private void OnShowEntitySuccess(object sender, GameEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void OnShowEntityFailure(object sender, GameEventArgs e)
        {
            //throw new NotImplementedException();
        }
    } 
}
