using Gameplay.Mechanics.Timer;
using Gameplay.Shooting.Factories;
using Gameplay.Shooting.Scriptables;
using UnityEngine;


namespace Gameplay.Shooting.Weapons
{
    public sealed class Mortar : Weapon
    {

        #region Fields

        private readonly MortarConfig _mortarConfig;
        private readonly MineFactory _mineFactory;

        #endregion


        #region CodeLife

        public Mortar(MortarConfig mortarConfig, MineFactory mineFactory, TimerFactory timerFactory)
        {
            _mortarConfig = mortarConfig;
            _mineFactory = mineFactory;
            CooldownTimer = timerFactory.Create(mortarConfig.Cooldown);
        }

        #endregion


        #region Methods

        public override void CommenceFiring(Vector2 minePosition, Quaternion turretDirection)
        {
            if (IsOnCooldown) return;

            _mineFactory.Create(minePosition, _mortarConfig.MineConfig);

            CooldownTimer.Start();
        }

        #endregion

    }
}