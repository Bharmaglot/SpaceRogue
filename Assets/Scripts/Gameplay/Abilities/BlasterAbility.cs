using Gameplay.Mechanics.Timer;
using SpaceRogue.Abstraction;
using SpaceRogue.Gameplay.Abilities.Scriptables;
using SpaceRogue.Gameplay.Shooting.Factories;


namespace SpaceRogue.Gameplay.Abilities
{
    public sealed class BlasterAbility : Ability
    {

        #region Fields

        private readonly BlasterAbilityConfig _blasterAbilityConfig;
        private readonly EntityViewBase _entityView;
        private readonly MineFactory _mineFactory;

        #endregion


        #region CodeLife

        public BlasterAbility(
            BlasterAbilityConfig blasterAbilityConfig,
            EntityViewBase entityView,
            TimerFactory timerFactory,
            MineFactory mineFactory) : base(blasterAbilityConfig, timerFactory)
        {
            _blasterAbilityConfig = blasterAbilityConfig;
            _entityView = entityView;
            _mineFactory = mineFactory;
        }

        #endregion


        #region Methods

        public override void UseAbility()
        {
            if (IsOnCooldown)
            {
                return;
            }

            _mineFactory.Create(_entityView.transform.position, _blasterAbilityConfig.MineConfig);

            CooldownTimer.Start();
        }

        #endregion

    }
}