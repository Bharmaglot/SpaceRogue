using SpaceRogue.Abstraction;
using SpaceRogue.Gameplay.Abilities.Scriptables;
using SpaceRogue.Player.Movement;
using Zenject;


namespace SpaceRogue.Gameplay.Abilities
{
    public sealed class UnitAbilityFactory : PlaceholderFactory<EntityViewBase, AbilityConfig, UnitMovement, IUnitAbilityInput, UnitAbility>
    {

        #region Fields

        private readonly AbilityFactory _abilityFactory;

        #endregion


        #region CodeLife

        public UnitAbilityFactory(AbilityFactory abilityFactory)
        {
            _abilityFactory = abilityFactory;
        }

        #endregion


        #region Methods

        public override UnitAbility Create(EntityViewBase entityView, AbilityConfig config, UnitMovement unitMovement, IUnitAbilityInput input)
        {
            var ability = config != null ? _abilityFactory.Create(config, entityView, unitMovement) : new NullAbility();
            return new UnitAbility(ability, input);
        }

        #endregion

    }
}