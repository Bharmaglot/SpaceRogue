using Gameplay.Mechanics.Timer;
using SpaceRogue.Abstraction;
using SpaceRogue.Gameplay.Abilities.Scriptables;
using UnityEngine;


namespace SpaceRogue.Gameplay.Abilities
{
    public sealed class BlasterAbility : Ability
    {

        #region Fields

        private readonly BlasterAbilityConfig _blasterAbilityConfig;
        private readonly EntityViewBase _entityView;

        #endregion


        #region CodeLife

        public BlasterAbility(
            BlasterAbilityConfig blasterAbilityConfig,
            EntityViewBase entityView,
            TimerFactory timerFactory) : base(blasterAbilityConfig, timerFactory)
        {
            _blasterAbilityConfig = blasterAbilityConfig;
            _entityView = entityView;
        }

        #endregion


        #region Methods

        public override void UseAbility()
        {
            if (IsOnCooldown)
            {
                return;
            }

            //TODO Ability
            Debug.Log($"Ability Used!");

            CooldownTimer.Start();
        }

        #endregion

    }
}