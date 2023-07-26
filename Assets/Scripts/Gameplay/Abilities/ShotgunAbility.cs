using Gameplay.Mechanics.Timer;
using SpaceRogue.Abstraction;
using SpaceRogue.Gameplay.Abilities.Scriptables;
using UnityEngine;


namespace SpaceRogue.Gameplay.Abilities
{
    public sealed class ShotgunAbility : Ability
    {

        #region Fields

        private readonly ShotgunAbilityConfig _shotgunAbilityConfig;
        private readonly EntityViewBase _entityView;

        #endregion


        #region CodeLife

        public ShotgunAbility(
            ShotgunAbilityConfig shotgunAbilityConfig,
            EntityViewBase entityView,
            TimerFactory timerFactory) : base(shotgunAbilityConfig, timerFactory)
        {
            _shotgunAbilityConfig = shotgunAbilityConfig;
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