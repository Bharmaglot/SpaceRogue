using Gameplay.Shooting.Factories;
using Gameplay.Shooting.Scriptables;
using SpaceRogue.Abstraction;
using Zenject;


namespace Gameplay.Shooting
{
    public sealed class UnitWeaponFactory : PlaceholderFactory<EntityViewBase, MountedWeaponConfig, IUnitWeaponInput, UnitWeapon>
    {
        private readonly MountedWeaponFactory _mountedWeaponFactory;

        public UnitWeaponFactory(MountedWeaponFactory mountedWeaponFactory)
        {
            _mountedWeaponFactory = mountedWeaponFactory;
        }

        public override UnitWeapon Create(EntityViewBase entityView, MountedWeaponConfig config, IUnitWeaponInput input)
        {
            var mountedWeapon = _mountedWeaponFactory.Create(config, entityView);
            return new UnitWeapon(mountedWeapon, input);
        }
    }
}