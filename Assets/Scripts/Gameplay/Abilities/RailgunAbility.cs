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
        private readonly AbilityViewFactory _abilityViewFactory;

        private AbilityView _currentAbilityView;

        #endregion


        #region CodeLife

        public RailgunAbility(
            RailgunAbilityConfig railgunAbilityConfig,
            EntityViewBase entityView,
            TimerFactory timerFactory,
            AbilityViewFactory abilityViewFactory) : base(railgunAbilityConfig, timerFactory)
        {
            _railgunAbilityConfig = railgunAbilityConfig;
            _entityView = entityView;
            _abilityViewFactory = abilityViewFactory;
        }

        #endregion


        #region Methods

        protected override void OnDispose() 
        {
            if (_currentAbilityView != null)
            {
                Object.Destroy(_currentAbilityView.gameObject);
            }
        }

        public override void UseAbility()
        {
            if (IsOnCooldown)
            {
                return;
            }

            if(_currentAbilityView != null)
            {
                Object.Destroy(_currentAbilityView.gameObject);
            }

            _currentAbilityView = CreateAbilityView();
            ShockwaveEffect();

            CooldownTimer.Start();
        }

        private AbilityView CreateAbilityView()
        {
            var abilityView = _abilityViewFactory.Create(_entityView.transform.position, _railgunAbilityConfig);
            
            if (abilityView is RailgunAbilityView railgunAbilityView)
            {
                railgunAbilityView.SetShockwaveEffectSettings(
                    _railgunAbilityConfig.ShockwaveGradient,
                    _railgunAbilityConfig.ShockwaveLifetime,
                    _railgunAbilityConfig.ShockwaveRadius);
            }

            return abilityView;
        }

        private void ShockwaveEffect()
        {
            var colliders = Physics2D.OverlapCircleAll(_entityView.transform.position, _railgunAbilityConfig.ShockwaveRadius);

            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent(out EntityViewBase entityViewBase))
                {
                    if (entityViewBase.EntityType == _entityView.EntityType)
                    {
                        continue;
                    }

                    var distance = entityViewBase.transform.position - _entityView.transform.position;
                    var length = distance.magnitude;

                    if (length > 0.0f)
                    {
                        var force = _railgunAbilityConfig.ShockwaveForce / length;
                        entityViewBase.Rigidbody2D.AddForce(distance.normalized * force, ForceMode2D.Impulse);
                    }
                }
            }
        }

        #endregion

    }
}