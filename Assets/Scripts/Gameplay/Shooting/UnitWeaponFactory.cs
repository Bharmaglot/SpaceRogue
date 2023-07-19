using SpaceRogue.Abstraction;
using SpaceRogue.Gameplay.Shooting.Factories;
using SpaceRogue.Gameplay.Shooting.Scriptables;
using Zenject;


namespace SpaceRogue.Gameplay.Shooting
{
    public sealed class UnitWeaponFactory : PlaceholderFactory<EntityViewBase, MountedWeaponConfig, IUnitWeaponInput, UnitWeapon>
    {
        #region Fields

        private readonly MountedWeaponFactory _mountedWeaponFactory;

        #endregion

        #region CodeLife

        public UnitWeaponFactory(MountedWeaponFactory mountedWeaponFactory)
        {
            _mountedWeaponFactory = mountedWeaponFactory;
        }

        #endregion

        #region Methods

        public override UnitWeapon Create(EntityViewBase entityView, MountedWeaponConfig config, IUnitWeaponInput input)
        {
            var mountedWeapon = _mountedWeaponFactory.Create(config, entityView);
            return new UnitWeapon(mountedWeapon, input);
        }

        #endregion
    }
}