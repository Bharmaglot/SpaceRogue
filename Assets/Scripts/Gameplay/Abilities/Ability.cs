using Gameplay.Mechanics.Timer;
using SpaceRogue.Abstraction;
using SpaceRogue.Gameplay.Abilities.Scriptables;
using System;


namespace SpaceRogue.Gameplay.Abilities
{
    public abstract class Ability : IDisposable, IDestroyable
    {

        #region Events

        public event Action AbilityUsed;

        public event Action AbilityAvailable;

        public event Action Destroyed;

        #endregion


        #region Properties

        public string AbilityName { get; private set; }
        protected Timer CooldownTimer { get; private set; }
        protected bool IsOnCooldown => CooldownTimer.InProgress;

        #endregion


        #region CodeLife

        public Ability() { }

        public Ability(AbilityConfig abilityConfig, TimerFactory timerFactory)
        {
            AbilityName = abilityConfig.Type.ToString();
            CooldownTimer = timerFactory.Create(abilityConfig.Cooldown);

            CooldownTimer.OnStart += OnAbilityUsed;
            CooldownTimer.OnExpire += OnAbilityAvailable;
        }

        public void Dispose()
        {
            OnDispose();
            
            if (CooldownTimer == null)
            {
                return;
            }

            CooldownTimer.OnStart -= OnAbilityUsed;
            CooldownTimer.OnExpire -= OnAbilityAvailable;

            CooldownTimer.Dispose();
            Destroyed?.Invoke();
        }

        #endregion


        #region Methods

        public abstract void UseAbility();

        protected virtual void OnDispose() { }

        private void OnAbilityUsed() => AbilityUsed?.Invoke();

        private void OnAbilityAvailable() => AbilityAvailable?.Invoke();

        #endregion

    }
}