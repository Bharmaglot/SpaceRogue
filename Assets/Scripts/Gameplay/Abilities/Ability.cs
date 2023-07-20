using Gameplay.Mechanics.Timer;
using System;


namespace SpaceRogue.Gameplay.Abilities
{
    public abstract class Ability : IDisposable
    {
        #region Properties

        protected Timer CooldownTimer { get; set; }
        protected bool IsOnCooldown => CooldownTimer.InProgress;

        #endregion

        #region CodeLife

        public void Dispose() => CooldownTimer.Dispose();

        #endregion

        #region Methods

        public abstract void UseAbility();

        #endregion
    }
}