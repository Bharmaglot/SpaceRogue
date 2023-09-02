using Gameplay.Mechanics.Timer;
using SpaceRogue.Gameplay.Shooting.Scriptables;
using SpaceRogue.Services;
using UnityEngine;


namespace SpaceRogue.Gameplay.Shooting
{
    public sealed class Rocket : BaseReactiveObject
    {

        #region Fields

        private readonly RocketConfig _rocketConfig;

        private readonly float _speed;
        private readonly float _rotateSpeed;

        #endregion


        #region CodeLife

        public Rocket(
            ReactiveObjectView view, 
            Updater updater, 
            TimerFactory timerFactory, 
            RocketConfig rocketConfig,
            InstantExplosionFactory instantExplosionFactory) : base(view,  updater, timerFactory, instantExplosionFactory)
        {

            _rocketConfig = rocketConfig;
            _targetUnitType = rocketConfig.TargetUnitType;

            _targetFinderView.TargetEntersTrigger += FindTarget;

            _timer = timerFactory.Create(rocketConfig.LifeTime);
            _timer.OnExpire += ExpolsionAfterLifeTime;
            _timer.Start();

            _speed = rocketConfig.Speed;
            _rotateSpeed = rocketConfig.RotateSpeed;
        }

        protected override void OnDispose()
        {
            _targetFinderView.TargetEntersTrigger -= FindTarget;
            UnityEngine.Object.Destroy(_reactiveObjectView.gameObject, Time.deltaTime);
        }

        #endregion


        #region Methods 

        protected override void ActiveExplosion()
        {
            _instantExplosionFactory.Create(_reactiveObjectView.gameObject.transform.position, _rocketConfig.ExplosionConfig as InstantExplosionConfig, this);
            base.ActiveExplosion();
        }

        protected override void RocketFly()
        {
            _reactiveObjectView.transform.Translate(Vector3.up * _speed * Time.deltaTime);
            if (_targetView != null)
                RocketTargeting();
        }

        private void RocketTargeting()
        {
            var direction = _targetView.transform.position - _reactiveObjectView.transform.position;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _reactiveObjectView.transform.rotation = Quaternion.Slerp(_reactiveObjectView.transform.rotation, Quaternion.AngleAxis(angle + ANGLE_CORRECTION, Vector3.forward), _rotateSpeed);
            
        }

        #endregion

    }
}