using Gameplay.Shooting.Scriptables;
using SpaceRogue.Enums;
using SpaceRogue.Services;
using SpaceRogue.Abstraction;
using System;
using UnityEngine;

namespace Gameplay.Shooting
{
    public class Mine : IDisposable
    {
        private readonly Updater _updater;

        private MineView _mineView;
        private MineAlertZoneView _mineAlertZoneView;
        private MineExploseView _mineExploseView;

        private Transform _mineBodyTransform;
        private Transform _mineAlertZoneTansform;
        private Transform _mineTimerVisualTransform;
        private Transform _explozionTransform;

        private float _timeToActiveAlarmSystem;
        private float _timeToExplosion;
        private float _damageFromExplosion;
        private float _speedWaveExplosion;
        private EntityType _targetUnitType;

        private float _stepTimerVisualTransform;

        internal Mine(Updater updater, MineView mineView, MineConfig mineConfig)
        {
            _updater = updater;

            _mineBodyTransform = mineView.MineBodyTransform;
            _mineAlertZoneTansform = mineView.MineAlertZoneTansform;
            _mineTimerVisualTransform = mineView.MineAlertZoneTansform;
            _explozionTransform = mineView.ExplozionTransform;


            _timeToActiveAlarmSystem = mineConfig.TimeToActiveAlarmSystem;
            _timeToExplosion = mineConfig.TimeToExplosion;
            _speedWaveExplosion = mineConfig.SpeedWaveExplosion;

            _damageFromExplosion = mineConfig.DamageFromExplosion;
     
            _targetUnitType = mineConfig.TargetUnitType;

            _mineAlertZoneView = mineView.MineAlertZoneView;
            _mineExploseView = mineView.MineExploseView;

            _stepTimerVisualTransform = (_mineAlertZoneTansform.localScale.x - _mineTimerVisualTransform.localScale.x) / _timeToExplosion * Time.deltaTime;

            _updater.SubscribeToUpdate(TimerToActiveAlarmZone);
        }

        private void TimerToActiveAlarmZone()
        {
            _timeToActiveAlarmSystem = _timeToActiveAlarmSystem - Time.deltaTime;
            if (_timeToActiveAlarmSystem <= 0)
            {
                _updater.UnsubscribeFromUpdate(TimerToActiveAlarmZone);
                _mineAlertZoneView.TargetEnterAlarmZone += StartExplosionTimer;
            }
        }
        private void StartExplosionTimer(EntityViewBase target)
        {

            if(target.EntityType == _targetUnitType)
            _mineAlertZoneTansform.gameObject.SetActive(true);
            _updater.SubscribeToUpdate(ActiveExplosionTimer);
        }

        

        private void ActiveExplosionTimer()
        {
            if (_mineTimerVisualTransform.localScale.x < _mineAlertZoneTansform.localScale.x)
            {
                _mineTimerVisualTransform.localScale = new Vector2(_mineTimerVisualTransform.localScale.x + _stepTimerVisualTransform, _mineTimerVisualTransform.localScale.y + _stepTimerVisualTransform);
            }
            else
            {
                _updater.UnsubscribeFromUpdate(ActiveExplosionTimer);
                _mineAlertZoneView.TargetEnterAlarmZone -= StartExplosionTimer;
                _mineExploseView.TargetEnterExploseZone += ExplosionDamage;
                
                _mineTimerVisualTransform.gameObject.SetActive(false);
                _mineAlertZoneTansform.gameObject.SetActive(false);
                _mineBodyTransform.gameObject.SetActive(false);
                _explozionTransform.gameObject.SetActive(true);
                
                _updater.SubscribeToUpdate(ExplosionEffect);
            }
        }

        private void ExplosionEffect()
        {
            if (_explozionTransform.localScale.x < _mineTimerVisualTransform.localScale.x)
            {
                _explozionTransform.localScale = new Vector2(_explozionTransform.localScale.x + _speedWaveExplosion * Time.deltaTime, _explozionTransform.localScale.y + _speedWaveExplosion * Time.deltaTime);
            }
            else
            {
                Dispose();
            }
        }

        private void ExplosionDamage(EntityViewBase target)
        {
            
        }

        public void Dispose()
        {
            _mineAlertZoneView.TargetEnterAlarmZone -= StartExplosionTimer;
            _mineExploseView.TargetEnterExploseZone -= ExplosionDamage;
            _updater.UnsubscribeFromUpdate(ExplosionEffect);
            _updater.UnsubscribeFromUpdate(ActiveExplosionTimer);
        }
    }
}
