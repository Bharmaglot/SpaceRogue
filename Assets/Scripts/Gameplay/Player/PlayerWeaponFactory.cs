using SpaceRogue.Gameplay.Shooting;
using SpaceRogue.Gameplay.Shooting.Scriptables;
using SpaceRogue.InputSystem;
using SpaceRogue.Player.Movement;
using System.Collections.Generic;
using Zenject;


namespace SpaceRogue.Gameplay.Player
{
    public sealed class PlayerWeaponFactory : PlaceholderFactory<PlayerView, UnitMovement, List<UnitWeapon>>
    {

        #region Fields

        private readonly UnitWeaponFactory _unitWeaponFactory;
        private readonly List<MountedWeaponConfig> _weaponConfigs;
        private readonly DiContainer _diContainer;

        #endregion


        #region CodeLife

        public PlayerWeaponFactory(UnitWeaponFactory unitWeaponFactory, List<MountedWeaponConfig> weaponConfigs, DiContainer diContainer)
        {
            _unitWeaponFactory = unitWeaponFactory;
            _weaponConfigs = weaponConfigs;
            _diContainer = diContainer;
        }

        #endregion


        #region Methods

        public override List<UnitWeapon> Create(PlayerView playerView, UnitMovement unitMovement)
        {
            var result = new List<UnitWeapon>();
            var playerInput = _diContainer.Resolve<PlayerInput>();

            foreach (var weaponConfig in _weaponConfigs)
            {
                var unitWeapon = _unitWeaponFactory.Create(playerView, weaponConfig, unitMovement, playerInput);
                unitWeapon.IsEnable = false;
                result.Add(unitWeapon);
            }

            result[^1].IsEnable = true;

            return result;
        }

        #endregion

    }
}