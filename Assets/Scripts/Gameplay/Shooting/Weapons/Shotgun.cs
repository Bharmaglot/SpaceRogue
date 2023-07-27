using Gameplay.Mechanics.Timer;
using SpaceRogue.Enums;
using SpaceRogue.Gameplay.Shooting.Factories;
using SpaceRogue.Gameplay.Shooting.Scriptables;
using UnityEngine;
using Utilities.Mathematics;


namespace SpaceRogue.Gameplay.Shooting.Weapons
{
    public sealed class Shotgun : Weapon
    {

        #region Fields

        private readonly ShotgunConfig _config;
        private readonly EntityType _entityType;
        private readonly ProjectileFactory _projectileFactory;

        #endregion


        #region CodeLife

        public Shotgun(
            ShotgunConfig config,
            EntityType entityType,
            ProjectileFactory projectileFactory,
            TimerFactory timerFactory) : base(config, timerFactory)
        {
            _config = config;
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

            FireMultipleProjectiles(bulletPosition, turretRotation, _config.PelletCount, _config.SprayAngle);

            CooldownTimer.Start();
        }

        private void FireMultipleProjectiles(Vector2 bulletPosition, Quaternion turretRotation, int count, float sprayAngle)
        {
            var minimumAngle = -sprayAngle / 2;
            var singlePelletAngle = sprayAngle / count;

            for (var i = 0; i < count; i++)
            {
                var minimumPelletAngle = minimumAngle + i * singlePelletAngle;
                var maximumPelletAngle = minimumAngle + (i + 1) * singlePelletAngle;

                var pelletAngle = RandomPicker.PickRandomBetweenTwoValues(minimumPelletAngle, maximumPelletAngle);
                var pelletRotation = turretRotation * Quaternion.AngleAxis(pelletAngle, Vector3.forward);

                _projectileFactory.Create(new ProjectileSpawnParams(bulletPosition, pelletRotation, _entityType, _config.ShotgunProjectile));
            }
        }

        #endregion

    }
}