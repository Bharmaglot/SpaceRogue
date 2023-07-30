using Gameplay.Movement;
using SpaceRogue.Abstraction;
using SpaceRogue.Player.Movement;
using System;
using Zenject;


namespace SpaceRogue.Gameplay.Player
{
    public sealed class PlayerMovementFactory : PlaceholderFactory<PlayerView, IUnitMovementInput, UnitMovementModel, UnitMovement>
    {

        #region Events

        public event Action<UnitMovement> PlayerMovementCreated;

        #endregion


        #region Fields

        private readonly UnitMovementFactory _unitMovementFactory;

        #endregion


        #region CodeLife

        public PlayerMovementFactory(UnitMovementFactory unitMovementFactory)
        {
            _unitMovementFactory = unitMovementFactory;
        }

        #endregion


        #region Methods

        public override UnitMovement Create(PlayerView playerView, IUnitMovementInput movementInput, UnitMovementModel model)
        {
            var playerMovement = _unitMovementFactory.Create(playerView, movementInput, model);
            PlayerMovementCreated?.Invoke(playerMovement);
            return playerMovement;
        }

        #endregion

    }
}