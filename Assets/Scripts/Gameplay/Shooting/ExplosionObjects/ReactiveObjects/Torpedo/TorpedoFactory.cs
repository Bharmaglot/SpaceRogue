using Gameplay.Mechanics.Timer;
using SpaceRogue.Abstraction;
using SpaceRogue.Gameplay.Shooting.Scriptables;
using SpaceRogue.Services;
using UnityEngine;
using Zenject;


namespace SpaceRogue.Gameplay.Shooting.Factories
{
    public class TorpedoFactory : PlaceholderFactory<Vector2, Quaternion, TorpedoConfig, InstantExplosionFactory, IDestroyable, Torpedo>
    {

        #region Fields

        private readonly RocketViewFactory _rocketViewFactory;
        private readonly TimerFactory _timerFactory;
        private readonly Updater _updater;

        #endregion


        #region CodeLife

        public TorpedoFactory(RocketViewFactory rocketViewFactory, TimerFactory timerFactory, Updater updater)
        {
            _rocketViewFactory = rocketViewFactory;
            _timerFactory = timerFactory;
            _updater = updater;
        }

        #endregion


        #region Methods

        public override Torpedo Create(Vector2 position, Quaternion quaternion, TorpedoConfig config, InstantExplosionFactory onlyExplosionFactory, IDestroyable destroyable)
        {
            var torpedoView = _rocketViewFactory.Create(position, quaternion, config);
            var torpedo = new Torpedo(torpedoView, _updater, _timerFactory, config, onlyExplosionFactory);
            return torpedo;
        }

        #endregion

    }
}
