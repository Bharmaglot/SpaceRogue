using Gameplay.Mechanics.Timer;
using SpaceRogue.Abstraction;
using SpaceRogue.Gameplay.Shooting.Scriptables;
using SpaceRogue.Services;
using UnityEngine;


namespace SpaceRogue.Gameplay.Shooting
{
    public sealed class Torpedo : BaseReactiveObject
    {

        #region Fields

        private readonly TorpedoConfig _torpedoConfig;

        private float _speed;
        private readonly float _reactivateSpeed;
        private readonly float _lifeTime;
        private readonly float _radarRadius;
        private readonly float _radarSignalSpeed;
        private readonly float _guardTime;

        #endregion


        #region CodeLife

        public Torpedo(ReactiveObjectView view,
            Updater updater,
            TimerFactory timerFactory,
            TorpedoConfig torpedoConfig,
            InstantExplosionFactory instantExplosionFactory) : base(view, updater, timerFactory, instantExplosionFactory)
        {
            _torpedoConfig = torpedoConfig;
            _targetUnitType = torpedoConfig.TargetUnitType;
            
            _lifeTime = torpedoConfig.LifeTime;
            _speed = torpedoConfig.Speed;
            _reactivateSpeed = torpedoConfig.ReactivateSpeed;

            _radarRadius = torpedoConfig.DistanceToTarget;
            _radarSignalSpeed = torpedoConfig.RadarSignalSpeed;
            _guardTime = torpedoConfig.GuardTime;
            

            _timer = timerFactory.Create(torpedoConfig.TimeToActiveGuard);
            _timer.OnExpire += EndFly;
            _timer.Start();

            _targetFinderView.gameObject.SetActive(false);
        }

        protected override void OnDispose()
        {
            _targetFinderView.TargetEntersTrigger -= FindTarget;
            _timer.OnExpire -= EndFly;
            _updater.UnsubscribeFromUpdate(ActiveRadarSignal);
            UnityEngine.Object.Destroy(_reactiveObjectView.gameObject, Time.deltaTime);
        }

        #endregion


        #region Methods

        protected override void ActiveExplosion()
        {
            _instantExplosionFactory.Create(_reactiveObjectView.gameObject.transform.position, _torpedoConfig.ExplosionConfig as InstantExplosionConfig, this);
            base.ActiveExplosion();
        }

        protected override void RocketFly()
        {
            _reactiveObjectView.transform.Translate(Vector3.up * _speed * Time.deltaTime);
        }

        protected override void TargetLocked(EntityViewBase target)
        {
            base.TargetLocked(target);
            _timer.OnExpire -= ExpolsionAfterLifeTime;
            _timer.SetToZero();
            AttackTarget();
        }

        private void AttackTarget()
        {
            RotateToTarget();

            _updater.UnsubscribeFromUpdate(ActiveRadarSignal);
            _updater.SubscribeToUpdate(RocketFly);

            _timer.SetMaxValue(_lifeTime);
            _timer.OnExpire += ExpolsionAfterLifeTime;
            _timer.Start();

        }

        private void RotateToTarget()
        {
            var direction = _targetView.transform.position - _reactiveObjectView.transform.position;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _reactiveObjectView.transform.rotation = Quaternion.AngleAxis(angle + ANGLE_CORRECTION, Vector3.forward);
        }

        private void EndFly()
        {
            _updater.UnsubscribeFromUpdate(RocketFly);
            _timer.OnExpire -= EndFly;
            _speed = _reactivateSpeed;
            ActiveGuardMode();
        }

        private void ActiveGuardMode()
        {
            _targetFinderView.gameObject.SetActive(true);
            _targetFinderView.TargetEntersTrigger += FindTarget;
            _updater.SubscribeToUpdate(ActiveRadarSignal);
            
            _timer.SetMaxValue(_guardTime);
            _timer.OnExpire += ExpolsionAfterLifeTime;
            _timer.Start();
        }

        private void ActiveRadarSignal()
        {
            if (_radarSignalSpeed <= 0) return;
            
            if (_targetFinderView.transform.localScale.x < _radarRadius)
            {
                _targetFinderView.transform.localScale = new Vector2(_targetFinderView.transform.localScale.x + _radarSignalSpeed * Time.deltaTime, _targetFinderView.transform.localScale.y + _radarSignalSpeed * Time.deltaTime);
            }
            else
            {
                _targetFinderView.transform.localScale = new Vector2(0, 0);
            }
        }

        #endregion;

    }
}