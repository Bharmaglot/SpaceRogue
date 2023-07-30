using Gameplay.Survival;
using SpaceRogue.Abstraction;
using System;
using Zenject;


namespace SpaceRogue.Gameplay.Player
{
    public sealed class PlayerSurvivalFactory : PlaceholderFactory<EntityViewBase, EntitySurvival>
    {

        #region Events

        public event Action<EntitySurvival> PlayerSurvivalCreated;

        #endregion


        #region Fields

        private readonly EntitySurvivalFactory _entitySurvivalFactory;
        private readonly EntitySurvivalConfig _playerSurvivalConfig;

        #endregion


        #region CodeLife

        public PlayerSurvivalFactory(EntitySurvivalFactory entitySurvivalFactory, EntitySurvivalConfig playerSurvivalConfig)
        {
            _entitySurvivalFactory = entitySurvivalFactory;
            _playerSurvivalConfig = playerSurvivalConfig;
        }

        #endregion


        #region Methods

        public override EntitySurvival Create(EntityViewBase view)
        {
            var playerSurvival = _entitySurvivalFactory.Create(view, _playerSurvivalConfig);
            PlayerSurvivalCreated?.Invoke(playerSurvival);
            return playerSurvival;
        }

        #endregion

    }
}