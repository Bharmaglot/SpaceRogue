using Gameplay.Mechanics.Timer;
using SpaceRogue.Gameplay.Shooting.Scriptables;
using System;
using UnityEngine;


namespace SpaceRogue.Gameplay.Shooting
{
    public abstract class Weapon : IDisposable
    {
        #region Events

        public event Action WeaponUsed;

        public event Action WeaponAvailable;

        #endregion

        #region Properties

        public string WeaponName { get; private set; }
        protected Timer CooldownTimer { get; private set; }
        protected bool IsOnCooldown => CooldownTimer.InProgress;

        #endregion

        #region CodeLife

        public Weapon() { }

        public Weapon(WeaponConfig weaponConfig, TimerFactory timerFactory)
        {
            WeaponName = weaponConfig.Type.ToString();
            CooldownTimer = timerFactory.Create(weaponConfig.Cooldown);

            CooldownTimer.OnStart += OnWeaponUsed;
            CooldownTimer.OnExpire += OnWeaponAvailable;
        }

        public void Dispose()
        {
            CooldownTimer.OnStart -= OnWeaponUsed;
            CooldownTimer.OnExpire -= OnWeaponAvailable;

            CooldownTimer.Dispose();
        }

        #endregion

        #region Methods

        public abstract void CommenceFiring(Vector2 bulletPosition, Quaternion turretRotation);

        private void OnWeaponUsed() => WeaponUsed?.Invoke();

        private void OnWeaponAvailable() => WeaponAvailable?.Invoke();

        #endregion
    }
}