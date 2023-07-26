using SpaceRogue.Gameplay.Shooting;
using SpaceRogue.Gameplay.Shooting.Scriptables;
using SpaceRogue.InputSystem;
using SpaceRogue.Player.Movement;
using System;
using Zenject;


namespace Gameplay.Player
{
    public sealed class PlayerWeaponFactory : PlaceholderFactory<PlayerView, UnitMovement, UnitWeapon>
    {

        #region Events

        public event Action<UnitWeapon> UnitWeaponCreated;

        #endregion


        #region Fields

        private readonly UnitWeaponFactory _unitWeaponFactory;
        private readonly MountedWeaponConfig _config;
        private readonly DiContainer _diContainer;

        #endregion


        #region CodeLife

        public PlayerWeaponFactory(UnitWeaponFactory unitWeaponFactory, MountedWeaponConfig config, DiContainer diContainer)
        {
            _unitWeaponFactory = unitWeaponFactory;
            _config = config;
            _diContainer = diContainer;
        }

        #endregion


        #region Methods

        public override UnitWeapon Create(PlayerView playerView, UnitMovement unitMovement)
        {
            var playerInput = _diContainer.Resolve<PlayerInput>();
            var unitWeapon = _unitWeaponFactory.Create(playerView, _config, unitMovement, playerInput);
            UnitWeaponCreated?.Invoke(unitWeapon);
            return unitWeapon;
        }

        #endregion

    }
}