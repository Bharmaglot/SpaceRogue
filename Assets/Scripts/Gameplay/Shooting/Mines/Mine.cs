using Gameplay.Damage;
using Gameplay.Mechanics.Timer;
using SpaceRogue.Abstraction;
using SpaceRogue.Enums;
using SpaceRogue.Gameplay.Shooting.Scriptables;
using SpaceRogue.Services;
using System;
using UnityEngine;


namespace SpaceRogue.Gameplay.Shooting
{
    public sealed class Mine : IDisposable
    {

        #region Fields

        private readonly Updater _updater;
        private readonly Timer _timerToActivateAlarmSystem;

        private readonly MineView _mineView;
        private readonly IDestroyable _destroyable;
        private readonly MineAlertZoneView _mineAlertZoneView;
        private readonly MineExplosionView _mineExplosionView;

        private readonly Transform _mineBodyTransform;
        private readonly Transform _mineAlertZoneTransform;
        private readonly Transform _mineTimerVisualTransform;
        private readonly Transform _explosionTransform;

        private readonly float _timeToExplosion;
        private readonly float _alertAndDamageAuraSize;
        private readonly float _speedWaveExplosion;
        private readonly float _stepTimerVisualTransform;
        private readonly EntityType _targetUnitType;

        #endregion


        #region CodeLife
        public Mine(Updater updater, MineView mineView, TimerFactory timerFactory, MineConfig mineConfig, IDestroyable destroyable)
        {
            _updater = updater;

            _mineBodyTransform = mineView.MineBodyTransform;
            _mineAlertZoneTransform = mineView.MineAlertZoneTransform;
            _mineTimerVisualTransform = mineView.MineTimerVisualTransform;
            _explosionTransform = mineView.ExplosionTransform;
            _mineView = mineView;
            _destroyable = destroyable;

            _destroyable.Destroyed += Dispose;
            _timeToExplosion = mineConfig.TimeToExplosion;
            _speedWaveExplosion = mineConfig.SpeedWaveExplosion;

            _alertAndDamageAuraSize = _mineAlertZoneTransform.localScale.x;

            _targetUnitType = mineConfig.TargetUnitType;

            _mineAlertZoneView = mineView.MineAlertZoneView;
            _mineExplosionView = mineView.MineExplosionView;
            _mineExplosionView.Init(new DamageModel(mineConfig.DamageFromExplosion));

            _stepTimerVisualTransform = (_mineAlertZoneTransform.localScale.x - _mineTimerVisualTransform.localScale.x) / _timeToExplosion * Time.deltaTime;

            _timerToActivateAlarmSystem = timerFactory.Create(mineConfig.TimeToActiveAlarmSystem);
            _timerToActivateAlarmSystem.OnExpire += TimerToActiveAlarmZone;

            _timerToActivateAlarmSystem.Start();
        }

        public void Dispose()
        {
            _destroyable.Destroyed -= Dispose;
            
            _timerToActivateAlarmSystem.OnExpire -= TimerToActiveAlarmZone;
            _timerToActivateAlarmSystem.Dispose();
            _mineAlertZoneView.TargetEnterAlarmZone -= StartExplosionTimer;

            _updater.UnsubscribeFromUpdate(ExplosionEffect);
            _updater.UnsubscribeFromUpdate(ActiveExplosionTimer);
            UnityEngine.Object.Destroy(_mineView.gameObject);
        }

        #endregion


        #region Methods

        private void TimerToActiveAlarmZone()
        {
            _timerToActivateAlarmSystem.OnExpire -= TimerToActiveAlarmZone;
            _timerToActivateAlarmSystem.Dispose();
            _mineAlertZoneTransform.gameObject.SetActive(true);
            _mineAlertZoneView.TargetEnterAlarmZone += StartExplosionTimer;
        }

        private void StartExplosionTimer(EntityViewBase target)
        {
            if (_targetUnitType.HasFlag(target.EntityType))
            {
                _mineAlertZoneView.TargetEnterAlarmZone -= StartExplosionTimer;
                _mineAlertZoneTransform.gameObject.SetActive(true);
                _mineTimerVisualTransform.gameObject.SetActive(true);
                _updater.SubscribeToUpdate(ActiveExplosionTimer);
            }
        }

        private void ActiveExplosionTimer()
        {
            if (_mineTimerVisualTransform.localScale.x < _alertAndDamageAuraSize)
            {
                _mineTimerVisualTransform.localScale = new Vector2(_mineTimerVisualTransform.localScale.x + _stepTimerVisualTransform, _mineTimerVisualTransform.localScale.y + _stepTimerVisualTransform);
            }
            else
            {
                _updater.UnsubscribeFromUpdate(ActiveExplosionTimer);
                _mineAlertZoneView.TargetEnterAlarmZone -= StartExplosionTimer;

                _mineTimerVisualTransform.gameObject.SetActive(false);
                _mineAlertZoneTransform.gameObject.SetActive(false);
                _mineBodyTransform.gameObject.SetActive(false);
                _explosionTransform.gameObject.SetActive(true);

                _updater.SubscribeToUpdate(ExplosionEffect);
            }
        }

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