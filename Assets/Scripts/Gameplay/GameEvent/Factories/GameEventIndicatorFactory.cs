using SpaceRogue.Scriptables.GameEvent;
using SpaceRogue.Services;
using UnityEngine;
using Zenject;


namespace SpaceRogue.Gameplay.GameEvent.Factories
{
    public sealed class GameEventIndicatorFactory : PlaceholderFactory<Collider2D, GameEventConfig, GameEventIndicator>
    {

        #region Fields

        private readonly Updater _updater;
        private readonly GameEventIndicatorView _gameEventIndicatorView;
        private readonly GameEventIndicatorViewFactory _gameEventIndicatorViewFactory;

        #endregion


        #region CodeLife

        public GameEventIndicatorFactory(Updater updater, GameEventIndicatorView gameEventIndicatorView, GameEventIndicatorViewFactory gameEventIndicatorViewFactory)
        {
            _updater = updater;
            _gameEventIndicatorView = gameEventIndicatorView;
            _gameEventIndicatorViewFactory = gameEventIndicatorViewFactory;
        }

        #endregion


        #region Methods

        public override GameEventIndicator Create(Collider2D collider, GameEventConfig gameEventConfig)
        {
            var indicatorView = _gameEventIndicatorViewFactory.Create(_gameEventIndicatorView);
            return new GameEventIndicator(_updater, indicatorView, collider, gameEventConfig);
        }

        #endregion

    }
}