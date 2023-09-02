using Gameplay.Mechanics.Timer;
using SpaceRogue.Abstraction;
using SpaceRogue.Enums;
using SpaceRogue.Gameplay.Abilities.Scriptables;
using SpaceRogue.Gameplay.Shooting.Factories;
using SpaceRogue.Player.Movement;
using System;
using Zenject;


namespace SpaceRogue.Gameplay.Abilities
{
    public sealed class AbilityFactory : IFactory<AbilityConfig, EntityViewBase, UnitMovement, Ability>
    {

        #region Fields

        private readonly TimerFactory _timerFactory;
        private readonly AbilityViewFactory _abilityViewFactory;
        private readonly GravitationMineFactory _gravitationMineFactory;
        private readonly MineFactory _mineFactory;
        private readonly InstantExplosionFactory _instantExplosionFactory;
        private readonly TorpedoFactory _torpedoFactory;

        #endregion


        #region CodeLife

        public AbilityFactory(TimerFactory timerFactory, AbilityViewFactory abilityViewFactory, 
            GravitationMineFactory gravitationMineFactory, MineFactory mineFactory, 
            TorpedoFactory torpedoFactory, InstantExplosionFactory instantExplosionFactory)
        {
            _timerFactory = timerFactory;
            _abilityViewFactory = abilityViewFactory;
            _gravitationMineFactory = gravitationMineFactory;
            _mineFactory = mineFactory;
            _instantExplosionFactory = instantExplosionFactory;
            _torpedoFactory = torpedoFactory;
        }

        #endregion


        #region Methods

        public Ability Create(AbilityConfig abilityConfig, EntityViewBase entityView, UnitMovement unitMovement) => abilityConfig.Type switch
        {
            AbilityType.None => new NullAbility(),
            AbilityType.BlasterAbility => new BlasterAbility(abilityConfig as BlasterAbilityConfig, entityView, _timerFactory, _mineFactory),
            AbilityType.ShotgunAbility => new ShotgunAbility(abilityConfig as ShotgunAbilityConfig, entityView, _timerFactory, _abilityViewFactory, _gravitationMineFactory),
            AbilityType.MinigunAbility => new MinigunAbility(abilityConfig as MinigunAbilityConfig, entityView, unitMovement, _timerFactory),
            AbilityType.RailgunAbility => new RailgunAbility(abilityConfig as RailgunAbilityConfig, entityView, _timerFactory, _abilityViewFactory),
            AbilityType.TorpedoAbility => new TorpedoAbility(abilityConfig as TorpedoAbilityConfig, _torpedoFactory, _timerFactory, _instantExplosionFactory, entityView),
            _ => throw new ArgumentOutOfRangeException(nameof(abilityConfig.Type), abilityConfig.Type, $"A not-existent ability type is provided")
        };

        #endregion

    }
}