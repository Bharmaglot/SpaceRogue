using SpaceRogue.Services;
using System;


namespace Gameplay.Survival.Health
{
    public sealed class EntityHealth : IDisposable
    {
        private readonly Updater _updater;
        private readonly float _healthRegenAmount;
        
        public event Action HealthReachedZero = () => { };
        public event Action HealthChanged = () => { };

        public float CurrentHealth { get; private set; }
        public float MaximumHealth { get; }
        
        
        public EntityHealth(IHealthInfo healthInfo, Updater updater)
        {
            _updater = updater;
            MaximumHealth = healthInfo.MaximumHealth;
            CurrentHealth = healthInfo.StartingHealth;
            _healthRegenAmount = healthInfo.HealthRegen;
            _updater.SubscribeToUpdate(RegenerateHealth);
        }

        public void Dispose()
        {
            _updater.UnsubscribeFromUpdate(RegenerateHealth);
        }

        internal void TakeDamage(float damageAmount)
        {
            if (damageAmount < 0) throw new ArgumentException("Damage cannot be less than zero!");
            if (damageAmount >= CurrentHealth)
            {
                CurrentHealth = 0.0f;
                HealthChanged.Invoke();
                HealthReachedZero.Invoke();
                return;
            }

            CurrentHealth -= damageAmount;
            HealthChanged.Invoke();
        }

        internal void Heal(float healingAmount)
        {
            if (healingAmount < 0) throw new ArgumentException("Healing cannot be less than zero!");
            CurrentHealth += healingAmount;
            if (CurrentHealth > MaximumHealth) CurrentHealth = MaximumHealth;
            HealthChanged.Invoke();
        }

        private void RegenerateHealth(float deltaTime)
        {
            if (CurrentHealth < MaximumHealth)
            {
                Heal(_healthRegenAmount * deltaTime);
            }
        }
    }
}