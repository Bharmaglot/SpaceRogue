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
        private readonly GravitationMineFactory _gravitationMineFactory;
        private readonly AbilityViewFactory _abilityViewFactory;

        #endregion


        #region CodeLife

        public ShotgunAbility(
            ShotgunAbilityConfig shotgunAbilityConfig,
            EntityViewBase entityView,
            TimerFactory timerFactory,
            AbilityViewFactory abilityViewFactory,
            GravitationMineFactory gravitationMineFactory) : base(shotgunAbilityConfig, timerFactory)
        {
            _shotgunAbilityConfig = shotgunAbilityConfig;
            _entityView = entityView;
            _abilityViewFactory = abilityViewFactory;
            _gravitationMineFactory = gravitationMineFactory;
        }

        #endregion


        #region Methods

        public override void UseAbility()
        {
            if (IsOnCooldown)
            {
                return;
            }

            var position = _entityView.transform.position + _entityView.transform.TransformDirection(Vector3.up * _shotgunAbilityConfig.Distance);

            var view = _abilityViewFactory.Create(position, _shotgunAbilityConfig);
            _gravitationMineFactory.Create(view, view.transform, _shotgunAbilityConfig);

            CooldownTimer.Start();
        }

        #endregion

    }
}