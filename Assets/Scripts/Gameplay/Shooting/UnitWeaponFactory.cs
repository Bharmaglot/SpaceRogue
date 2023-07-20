using SpaceRogue.Abstraction;
using SpaceRogue.Gameplay.Abilities;
using SpaceRogue.Gameplay.Shooting.Factories;
using SpaceRogue.Gameplay.Shooting.Scriptables;
using Zenject;


namespace SpaceRogue.Gameplay.Shooting
{
    public sealed class UnitWeaponFactory : PlaceholderFactory<EntityViewBase, MountedWeaponConfig, IUnitWeaponInput, UnitWeapon>
    {
        #region Fields

        private readonly MountedWeaponFactory _mountedWeaponFactory;
        private readonly AbilityFactory _abilityFactory;

        #endregion

        #region CodeLife

        public UnitWeaponFactory(MountedWeaponFactory mountedWeaponFactory, AbilityFactory abilityFactory)
        {
            _mountedWeaponFactory = mountedWeaponFactory;
            _abilityFactory = abilityFactory;
        }

        #endregion

        #region Methods

        public override UnitWeapon Create(EntityViewBase entityView, MountedWeaponConfig config, IUnitWeaponInput input)
        {
            var mountedWeapon = _mountedWeaponFactory.Create(config, entityView);
            var ability = config.Ability != null ? _abilityFactory.Create(config.Ability, entityView) : new NullAbility();
            return new UnitWeapon(mountedWeapon, ability, input);
        }

        #endregion
    }
}