using Gameplay.Mechanics.Timer;
using SpaceRogue.Abstraction;
using SpaceRogue.Gameplay.Abilities.Scriptables;
using UnityEngine;


namespace SpaceRogue.Gameplay.Abilities
{
    public sealed class MinigunAbility : Ability
    {
        #region Fields

        private readonly MinigunAbilityConfig _minigunAbility;
        private readonly EntityViewBase _entityView;

        #endregion

        #region CodeLife

        public MinigunAbility(
            MinigunAbilityConfig minigunAbilityConfig,
            EntityViewBase entityView,
            TimerFactory timerFactory) : base(minigunAbilityConfig, timerFactory)
        {
            _minigunAbility = minigunAbilityConfig;
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