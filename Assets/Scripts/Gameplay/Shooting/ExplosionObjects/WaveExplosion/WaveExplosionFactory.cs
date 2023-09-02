using SpaceRogue.Abstraction;
using SpaceRogue.Gameplay.Shooting.Scriptables;
using SpaceRogue.Services;
using UnityEngine;
using Zenject;


namespace SpaceRogue.Gameplay.Shooting.Factories
{
    public sealed class WaveExplosionFactory : PlaceholderFactory<Vector2, WaveExplosionConfig, IDestroyable, WaveExplosion>
    {

        #region Fields

        private readonly ExplosionViewFactory _viewFactory;
        private readonly Updater _updater;

        #endregion


        #region CodeLife

        public WaveExplosionFactory(ExplosionViewFactory viewFactory, Updater updater)
        {
            _viewFactory = viewFactory;
            _updater = updater;
        }

        #endregion


        #region Methods

        public override WaveExplosion Create(Vector2 position, WaveExplosionConfig config, IDestroyable destroyable)
        {
            var view = _viewFactory.Create(position, config);
            var explosion = new WaveExplosion(_updater, view, config, destroyable);
            return explosion;
        }

        #endregion

    }
}