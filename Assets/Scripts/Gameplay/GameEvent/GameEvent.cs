using Gameplay.Mechanics.Timer;
using SpaceRogue.Scriptables.GameEvent;
using System;
using UnityEngine;
using Utilities.Mathematics;


namespace SpaceRogue.Gameplay.GameEvent
{
    public abstract class GameEvent : IDisposable
    {

        #region Events

        public event Action<Collider2D, GameEventConfig> CreateIndicator;
        public event Action<GameEvent> GameEventDisposed;

        #endregion


        #region Fields

        protected readonly GameEventConfig Config;
        protected readonly Timer GameEventTimer;

        private bool _isOnceSuccessfully;

        #endregion


        #region CodeLife

        public GameEvent() { }

        public GameEvent(GameEventConfig config, TimerFactory timerFactory)
        {
            Config = config;
            GameEventTimer = timerFactory.Create(config.ResponseTime);

            GameEventTimer.OnExpire += CheckEvent;
            GameEventTimer.Start();
        }

        public void Dispose()
        {
            OnDispose();

            GameEventTimer.OnExpire -= CheckEvent;
            GameEventTimer.Dispose();

            GameEventDisposed?.Invoke(this);
        }

        #endregion


        #region Methods

        protected virtual void OnDispose() { }

        protected abstract bool RunGameEvent();

        protected void AddGameEventIndicator(Collider2D collider) => CreateIndicator?.Invoke(collider, Config);

        private void CheckEvent()
        {
            if (RandomPicker.TakeChance(Config.Chance))
            {
                _isOnceSuccessfully = RunGameEvent();
            }

            if (Config.IsRecurring || !_isOnceSuccessfully)
            {
                GameEventTimer.Start();
                return;
            }

            if (!Config.IsRecurring && _isOnceSuccessfully)
            {
                GameEventTimer.OnExpire -= CheckEvent;
            }
        }

        #endregion

    }
}