using Gameplay.Mechanics.Timer;
using SpaceRogue.Abstraction;
using SpaceRogue.Gameplay.Shooting.Scriptables;
using SpaceRogue.Services;
using UnityEngine;
using Zenject;


namespace SpaceRogue.Gameplay.Shooting.Factories
{
    public sealed class RocketFactory : PlaceholderFactory<Vector2, Quaternion, RocketConfig, InstantExplosionFactory, IDestroyable,  Rocket>
    {

        #region Fields

        private readonly RocketViewFactory _rocketViewFactory;
        private readonly TimerFactory _timerFactory;
        private readonly Updater _updater;

        #endregion


        #region CodeLife

        public RocketFactory(RocketViewFactory rocketViewFactory, TimerFactory timerFactory, Updater updater)
        {
            _rocketViewFactory = rocketViewFactory;
            _timerFactory = timerFactory;
            _updater = updater;
        }

        #endregion


        #region Methods

        public override Rocket Create(Vector2 position, Quaternion quaternion, RocketConfig config,  InstantExplosionFactory instantExplosionFactory, IDestroyable destroyable)
        {
            var rocketView = _rocketViewFactory.Create(position, quaternion, config);
            var rocket = new Rocket(rocketView, _updater, _timerFactory, config, instantExplosionFactory);
            return rocket;
        }

        #endregion
    }
}
