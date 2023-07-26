using Gameplay.Mechanics.Timer;
using SpaceRogue.Enums;
using SpaceRogue.Gameplay.Shooting.Factories;
using SpaceRogue.Gameplay.Shooting.Scriptables;
using UnityEngine;


namespace SpaceRogue.Gameplay.Shooting.Weapons
{
    public sealed class Blaster : Weapon
    {

        #region Fields

        private readonly BlasterConfig _blasterConfig;
        private readonly EntityType _entityType;
        private readonly ProjectileFactory _projectileFactory;

        #endregion


        #region CodeLife

        public Blaster(
            BlasterConfig blasterConfig,
            EntityType entityType,
            ProjectileFactory projectileFactory,
            TimerFactory timerFactory) : base(blasterConfig, timerFactory)
        {
            _blasterConfig = blasterConfig;
            _entityType = entityType;
            _projectileFactory = projectileFactory;
        }

        #endregion


        #region Methods

        public override void CommenceFiring(Vector2 bulletPosition, Quaternion turretRotation)
        {
            if (IsOnCooldown)
            {
                return;
            }

            _projectileFactory.Create(new ProjectileSpawnParams(bulletPosition, turretRotation, _entityType, _blasterConfig.BlasterProjectile));

            CooldownTimer.Start();
        }

        #endregion

    }
}