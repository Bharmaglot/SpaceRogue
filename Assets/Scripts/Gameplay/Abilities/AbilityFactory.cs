using Gameplay.Mechanics.Timer;
using SpaceRogue.Abstraction;
using SpaceRogue.Enums;
using SpaceRogue.Gameplay.Abilities.Scriptables;
using SpaceRogue.Player.Movement;
using System;
using Zenject;


namespace SpaceRogue.Gameplay.Abilities
{
    public sealed class AbilityFactory : IFactory<AbilityConfig, EntityViewBase, UnitMovement, Ability>
    {
        #region Fields

        private readonly TimerFactory _timerFactory;

        #endregion

        #region CodeLife

        public AbilityFactory(TimerFactory timerFactory)
        {
            _timerFactory = timerFactory;
        }

        #endregion

        #region Methods

        public Ability Create(AbilityConfig abilityConfig, EntityViewBase entityView, UnitMovement unitMovement) => abilityConfig.Type switch
        {
            AbilityType.None => new NullAbility(),
            AbilityType.BlasterAbility => new BlasterAbility(abilityConfig as BlasterAbilityConfig, entityView, _timerFactory),
            AbilityType.ShotgunAbility => new ShotgunAbility(abilityConfig as ShotgunAbilityConfig, entityView, _timerFactory),
            AbilityType.MinigunAbility => new MinigunAbility(abilityConfig as MinigunAbilityConfig, entityView, unitMovement, _timerFactory),
            AbilityType.RailgunAbility => new RailgunAbility(abilityConfig as RailgunAbilityConfig, entityView, _timerFactory),
            _ => throw new ArgumentOutOfRangeException(nameof(abilityConfig.Type), abilityConfig.Type, $"A not-existent ability type is provided")
        };

        #endregion
    }
}