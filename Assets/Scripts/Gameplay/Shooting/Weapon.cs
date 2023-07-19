using Gameplay.Mechanics.Timer;
using System;
using UnityEngine;


namespace SpaceRogue.Gameplay.Shooting
{
    public abstract class Weapon : IDisposable
    {
        #region Properties

        protected Timer CooldownTimer { get; set; }
        protected bool IsOnCooldown => CooldownTimer.InProgress;

        #endregion

        #region CodeLife

        public void Dispose() => CooldownTimer.Dispose();

        #endregion

        #region Methods

        public abstract void CommenceFiring(Vector2 bulletPosition, Quaternion turretRotation);

        #endregion
    }
}