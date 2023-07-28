using Gameplay.Mechanics.Timer;
using SpaceRogue.Gameplay.Shooting.Factories;
using SpaceRogue.Gameplay.Shooting.Scriptables;
using UnityEngine;


namespace SpaceRogue.Gameplay.Shooting.Weapons
{
    public sealed class Mortar : Weapon
    {

        #region Fields

        private readonly MortarConfig _mortarConfig;
        private readonly MineFactory _mineFactory;

        #endregion


        #region CodeLife

        public Mortar(
            MortarConfig mortarConfig,
            MineFactory mineFactory,
            TimerFactory timerFactory) : base(mortarConfig, timerFactory)
        {
            _mortarConfig = mortarConfig;
            _mineFactory = mineFactory;
        }

        #endregion


        #region Methods

        public override void CommenceFiring(Vector2 minePosition, Quaternion turretDirection)
        {
            if (IsOnCooldown)
            {
                return;
            }

            _mineFactory.Create(minePosition, _mortarConfig.MineConfig, this);

            CooldownTimer.Start();
        }

        #endregion

    }
}