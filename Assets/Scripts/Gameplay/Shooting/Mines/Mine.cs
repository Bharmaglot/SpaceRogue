using Gameplay.Shooting.Scriptables;
using SpaceRogue.Enums;
using SpaceRogue.Services;
using SpaceRogue.Abstraction;
using System;
using UnityEngine;
using Gameplay.Damage;
using Gameplay.Mechanics.Timer;


namespace SpaceRogue.Shooting
{
    public sealed class Mine : IDisposable
    {

        #region Fields

        private readonly Updater _updater;
        private readonly Timer _timerToActivateAlarmSystem;

        private MineView _mineView;
        private MineAlertZoneView _mineAlertZoneView;
        private MineExploseView _mineExploseView;

        private Transform _mineBodyTransform;
        private Transform _mineAlertZoneTansform;
        private Transform _mineTimerVisualTransform;
        private Transform _explozionTransform;

        private float _timeToExplosion;
        private float _alertAndDamageAuraSize;
        private float _speedWaveExplosion;
        private float _stepTimerVisualTransform;
        private EntityType _targetUnitType;

        #endregion


        #region CodeLife
        public Mine(Updater updater, MineView mineView, TimerFactory timerFactory, MineConfig mineConfig)
        {
            _updater = updater;

            _mineBodyTransform = mineView.MineBodyTransform;
            _mineAlertZoneTansform = mineView.MineAlertZoneTansform;
            _mineTimerVisualTransform = mineView.MineTimerVisualTransform;
            _explozionTransform = mineView.ExplozionTransform;
            _mineView = mineView;

            _timeToExplosion = mineConfig.TimeToExplosion;
            _speedWaveExplosion = mineConfig.SpeedWaveExplosion;

            _alertAndDamageAuraSize = _mineAlertZoneTansform.localScale.x;

            _targetUnitType = mineConfig.TargetUnitType;

            _mineAlertZoneView = mineView.MineAlertZoneView;
            _mineExploseView = mineView.MineExploseView;
            _mineExploseView.Init(new DamageModel(mineConfig.DamageFromExplosion));

            _stepTimerVisualTransform = (_mineAlertZoneTansform.localScale.x - _mineTimerVisualTransform.localScale.x) / _timeToExplosion * Time.deltaTime;

            _timerToActivateAlarmSystem = timerFactory.Create(mineConfig.TimeToActiveAlarmSystem);
            _timerToActivateAlarmSystem.OnExpire += TimerToActiveAlarmZone;

            _timerToActivateAlarmSystem.Start();
        }

        public void Dispose()
        {
            _updater.UnsubscribeFromUpdate(ExplosionEffect);
            _updater.UnsubscribeFromUpdate(ActiveExplosionTimer);
            GameObject.Destroy(_mineView.gameObject);
        }

        #endregion


        #region Methods

        private void TimerToActiveAlarmZone()
        {
            _timerToActivateAlarmSystem.OnExpire -= TimerToActiveAlarmZone;
            _timerToActivateAlarmSystem.Dispose();
            _mineAlertZoneTansform.gameObject.SetActive(true);
            _mineAlertZoneView.TargetEnterAlarmZone += StartExplosionTimer;
        }

        private void StartExplosionTimer(EntityViewBase target)
        {
            if (_targetUnitType.HasFlag(target.EntityType))
            {
                _mineAlertZoneView.TargetEnterAlarmZone -= StartExplosionTimer;
                _mineAlertZoneTansform.gameObject.SetActive(true);
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
                _mineAlertZoneTansform.gameObject.SetActive(false);
                _mineBodyTransform.gameObject.SetActive(false);
                _explozionTransform.gameObject.SetActive(true);

                _updater.SubscribeToUpdate(ExplosionEffect);
            }
        }

        private void ExplosionEffect()
        {
            if (_explozionTransform.localScale.x < _alertAndDamageAuraSize)
            {
                _explozionTransform.localScale = new Vector2(_explozionTransform.localScale.x + _speedWaveExplosion * Time.deltaTime, _explozionTransform.localScale.y + _speedWaveExplosion * Time.deltaTime);
            }
            else
            {
                Dispose();
            }
        }

        #endregion

    }
}