using SpaceRogue.Gameplay.GameEvent.Scriptables;
using SpaceRogue.Services;
using UnityEngine;
using Zenject;


namespace SpaceRogue.Gameplay.GameEvent.Factories
{
    public sealed class CometFactory : PlaceholderFactory<CometConfig, Vector2, Vector3, Comet.Comet>
    {

        #region Fields

        private readonly Updater _updater;
        private readonly CometViewFactory _cometViewFactory;

        #endregion


        #region CodeLife

        public CometFactory(Updater updater, CometViewFactory cometViewFactory)
        {
            _updater = updater;
            _cometViewFactory = cometViewFactory;
        }

        #endregion


        #region Methods

        public override Comet.Comet Create(CometConfig cometConfig, Vector2 position, Vector3 direction)
        {
            var cometView = _cometViewFactory.Create(cometConfig.CometView, position, cometConfig.Size);
            cometView.Init(new(cometConfig.Damage));
            var movementDirection = cometConfig.CometView.transform.TransformDirection(direction);
            return new(_updater, cometConfig, cometView, movementDirection);
        }

        #endregion

    }
}