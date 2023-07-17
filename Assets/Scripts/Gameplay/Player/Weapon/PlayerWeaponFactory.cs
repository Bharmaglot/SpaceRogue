using Gameplay.Shooting;
using Gameplay.Shooting.Scriptables;
using SpaceRogue.InputSystem;
using Zenject;


namespace Gameplay.Player.Weapon
{
    public sealed class PlayerWeaponFactory : PlaceholderFactory<PlayerView, UnitWeapon>
    {
        private readonly UnitWeaponFactory _unitWeaponFactory;
        private readonly MountedWeaponConfig _config;
        private readonly DiContainer _diContainer;

        public PlayerWeaponFactory(UnitWeaponFactory unitWeaponFactory, MountedWeaponConfig config, DiContainer diContainer)
        {
            _unitWeaponFactory = unitWeaponFactory;
            _config = config;
            _diContainer = diContainer;
        }

        public override UnitWeapon Create(PlayerView playerView)
        {
            var playerInput = _diContainer.Resolve<PlayerInput>();
            return _unitWeaponFactory.Create(playerView, _config, playerInput);
        }
    }
}