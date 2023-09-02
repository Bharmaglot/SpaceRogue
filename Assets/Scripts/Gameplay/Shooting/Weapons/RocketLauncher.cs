using Gameplay.Mechanics.Timer;
using SpaceRogue.Gameplay.Shooting.Factories;
using SpaceRogue.Gameplay.Shooting.Scriptables;
using UnityEngine;


namespace SpaceRogue.Gameplay.Shooting.Weapons
{
    public sealed class RocketLauncher : Weapon
    {

        #region Fields

        private readonly RocketLauncherConfig _rocketLauncherConfig;
        private readonly RocketFactory _rocketFactory;
        private readonly InstantExplosionFactory _instantExplosionFactory;

        #endregion


        #region CodeLife

        public RocketLauncher(
            RocketLauncherConfig rocketLauncherConfig,
            RocketFactory rocketFactory,
            TimerFactory timerFactory,
            InstantExplosionFactory instantExplosionFactory) : base(rocketLauncherConfig, timerFactory)
        {
            _rocketLauncherConfig = rocketLauncherConfig;
            _rocketFactory = rocketFactory;
            _instantExplosionFactory = instantExplosionFactory;
        }

        #endregion


        #region Methods

        public override void CommenceFiring(Vector2 bulletPosition, Quaternion turretRotation)
        {
            

            if (IsOnCooldown)
            {
                return;
            }
            _rocketFactory.Create(bulletPosition, turretRotation, _rocketLauncherConfig.RocketConfig, _instantExplosionFactory, this);

            CooldownTimer.Start();
        }

        #endregion
    }
}