using SpaceRogue.Gameplay.GameEvent.Comet;
using SpaceRogue.Gameplay.Pooling;
using UnityEngine;
using Zenject;


namespace SpaceRogue.Gameplay.GameEvent.Factories
{
    public sealed class CometViewFactory : PlaceholderFactory<CometView, Vector2, float, CometView>
    {

        #region Fields

        private readonly DiContainer _diContainer;
        private readonly Transform _gameEventPoolTransform;

        #endregion


        #region CodeLife

        public CometViewFactory(DiContainer diContainer, GameEventPool gameEventPool)
        {
            _diContainer = diContainer;
            _gameEventPoolTransform = gameEventPool.transform;
        }

        #endregion


        #region Methods

        public override CometView Create(CometView view, Vector2 position, float size)
        {
            var cometView = _diContainer.InstantiatePrefabForComponent<CometView>(view, position, Quaternion.identity, _gameEventPoolTransform);
            cometView.transform.localScale *= size;
            cometView.TrailRenderer.widthMultiplier += size;
            return cometView;
        }

        #endregion

    }
}