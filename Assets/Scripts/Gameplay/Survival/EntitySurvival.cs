using Gameplay.Damage;
using Gameplay.Survival.DamageImmunityFrame;
using Gameplay.Survival.Health;
using Gameplay.Survival.Shield;
using SpaceRogue.Abstraction;
using System;
using Utilities.Mathematics;


namespace Gameplay.Survival
{
    public sealed class EntitySurvival : IDisposable
    {

        #region Events

        public event Action UnitDestroyed;

        #endregion


        #region Fields

        private readonly EntityViewBase _entityView;
        private readonly EntityDamageImmunityFrame _entityDamageImmunityFrame;

        #endregion


        #region Properties

        public bool CanReceiveDamage { get; set; }

        public EntityHealth EntityHealth { get; }
        
        public EntityShield EntityShield { get; }

        #endregion


        #region CodeLife

        public EntitySurvival(EntityViewBase entityView, EntityHealth entityHealth, EntityShield entityShield, EntityDamageImmunityFrame entityDamageImmunityFrame)
        {
            EntityHealth = entityHealth;
            EntityShield = entityShield;
            _entityView = entityView;
            _entityDamageImmunityFrame = entityDamageImmunityFrame;

            CanReceiveDamage = true;
            EntityHealth.HealthReachedZero += OnHealthReachedZero;
            _entityView.DamageTaken += ReceiveDamage;
        }

        public void Dispose()
        {
            if (_entityView != null)
            {
                _entityView.DamageTaken -= ReceiveDamage;
                EntityHealth.HealthReachedZero -= OnHealthReachedZero;
            }

            EntityHealth.Dispose();
            EntityShield?.Dispose();
            _entityDamageImmunityFrame?.Dispose();
        }

        #endregion


        #region Methods

        internal void ReceiveDamage(DamageModel damageModel)
        {
            if (!CanReceiveDamage || damageModel.EntityType == _entityView.EntityType)
            {
                return;
            }

            var damage = RandomPicker.PickRandomBetweenTwoValues(damageModel.MinDamage, damageModel.MaxDamage);
            TakeDamage(damage);
        }

        internal void TakeDamage(float damageAmount)
        {
            if (_entityDamageImmunityFrame is not null && _entityDamageImmunityFrame.TryBlockDamage())
            {
                return;
            }

            if (EntityShield is null || EntityShield.CurrentShield == 0.0f)
            {
                TakeFullDamageToHealth(damageAmount);
            }
            else
            {
                TakeDamageToShieldThenHealth(damageAmount);
            }
        }

        internal void RestoreHealth(float healthAmount) => EntityHealth.Heal(healthAmount);

        private void TakeFullDamageToHealth(float damageAmount) => EntityHealth.TakeDamage(damageAmount);

        private void TakeDamageToShieldThenHealth(float damageAmount)
        {
            EntityShield.TakeDamage(damageAmount, out var remainingDamage);
            if (remainingDamage > 0.0f)
            {
                EntityHealth.TakeDamage(remainingDamage);
            }
        }

        private void OnHealthReachedZero() => UnitDestroyed?.Invoke();

        #endregion
    }
}