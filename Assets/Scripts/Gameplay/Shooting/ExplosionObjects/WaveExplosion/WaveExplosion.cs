using Gameplay.Damage;
using SpaceRogue.Abstraction;
using SpaceRogue.Gameplay.Shooting.Scriptables;
using SpaceRogue.Services;
using System;
using UnityEngine;


namespace SpaceRogue.Gameplay.Shooting
{
    public sealed class WaveExplosion : IDisposable
    {

        #region Fields

        private readonly Transform _explosionTransform;

        private readonly Updater _updater;

        private readonly ExplosionView _view;
        private readonly IDestroyable _destroyable;
        private readonly DamageExplosionView _explosionView;

        private readonly float _alertAndDamageAuraSize;
        private readonly float _speedWaveExplosion;

        #endregion


        #region CodeLife

        public WaveExplosion(Updater updater, ExplosionView view, WaveExplosionConfig config, IDestroyable destroyable)
        {
            _updater = updater;

            _alertAndDamageAuraSize = config.DamageRadiusSize;
            _speedWaveExplosion = config.SpeedWaveExplosion;


            _explosionTransform = view.ExplosionTransform;
            _view = view;

            _destroyable = destroyable;
            _destroyable.Destroyed += Dispose;

            _explosionView = view.DamageExplosionView;
            _explosionView.Init(new DamageModel(config.DamageFromExplosion));

            _updater.SubscribeToUpdate(ExplosionEffect);
        }

        public void Dispose()
        {
            _destroyable.Destroyed -= Dispose;

            _updater.UnsubscribeFromUpdate(ExplosionEffect);
            UnityEngine.Object.Destroy(_view.gameObject);
        }

        #endregion


        #region Methods

        private void ExplosionEffect()
        {
            if (_explosionTransform.localScale.x < _alertAndDamageAuraSize)
            {
                _explosionTransform.localScale = new Vector2(_explosionTransform.localScale.x + _speedWaveExplosion * Time.deltaTime, _explosionTransform.localScale.y + _speedWaveExplosion * Time.deltaTime);
            }
            else
            {
                Dispose();
            }
        }

        #endregion

    }
}
