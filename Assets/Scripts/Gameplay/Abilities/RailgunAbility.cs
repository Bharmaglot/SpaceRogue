using Gameplay.Mechanics.Timer;
using SpaceRogue.Abstraction;
using SpaceRogue.Gameplay.Abilities.Scriptables;
using UnityEngine;


namespace SpaceRogue.Gameplay.Abilities
{
    public sealed class RailgunAbility : Ability
    {
        #region Fields

        private readonly RailgunAbilityConfig _railgunAbilityConfig;
        private readonly EntityViewBase _entityView;

        #endregion

        #region CodeLife

        public RailgunAbility(
            RailgunAbilityConfig railgunAbilityConfig,
            EntityViewBase entityView,
            TimerFactory timerFactory) : base(railgunAbilityConfig, timerFactory)
        {
            _railgunAbilityConfig = railgunAbilityConfig;
            _entityView = entityView;
        }

        #endregion

        #region Methods

        public override void UseAbility()
        {
            if (IsOnCooldown) return;

            //TODO Ability
            Debug.Log($"Ability Used!");

            CooldownTimer.Start();
        }

        #endregion
    }
}