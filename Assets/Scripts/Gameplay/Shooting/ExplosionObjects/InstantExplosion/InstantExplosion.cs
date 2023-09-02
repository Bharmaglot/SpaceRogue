using Gameplay.Damage;
using Gameplay.Mechanics.Timer;
using SpaceRogue.Abstraction;
using System;
using UnityEngine;


namespace SpaceRogue.Gameplay.Shooting
{
    public class InstantExplosion : IDisposable
    {

        #region Fields

        private readonly ExplosionView _view;
        private readonly IDestroyable _destroyable;
        private readonly DamageExplosionView _explosionView;

        private readonly Timer _timer;

        #endregion


        #region CodeLife

        public InstantExplosion(TimerFactory timerFactory, ExplosionView view, InstantExplosionConfig config, IDestroyable destroyable)
        {
 
            _view = view;

            _destroyable = destroyable;
            _destroyable.Destroyed += Dispose;

            _explosionView = view.DamageExplosionView;
            _explosionView.Init(new DamageModel(config.DamageFromExplosion));

            _timer = timerFactory.Create(config.LifeTime);
            _timer.OnExpire += Dispose;
            _timer.Start();
        }

        public void Dispose()
        {
            _timer.Dispose();
            UnityEngine.Object.Destroy(_view.gameObject, Time.deltaTime);
        }

        #endregion

    }
}