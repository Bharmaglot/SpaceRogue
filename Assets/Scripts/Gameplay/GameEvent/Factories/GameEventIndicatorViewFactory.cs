using SpaceRogue.UI.Game;
using UnityEngine;
using Zenject;


namespace SpaceRogue.Gameplay.GameEvent.Factories
{
    public sealed class GameEventIndicatorViewFactory : PlaceholderFactory<GameEventIndicatorView, GameEventIndicatorView>
    {

        #region Fields

        private readonly DiContainer _diContainer;
        private readonly Transform _indicatorsViewTransform;

        #endregion


        #region CodeLife

        public GameEventIndicatorViewFactory(DiContainer diContainer, IndicatorsView indicatorsView)
        {
            _diContainer = diContainer;
            _indicatorsViewTransform = indicatorsView.transform;
        }

        #endregion


        #region Methods

        public override GameEventIndicatorView Create(GameEventIndicatorView view) 
            => _diContainer.InstantiatePrefabForComponent<GameEventIndicatorView>(view, _indicatorsViewTransform);

        #endregion

    }
}