using Gameplay.Mechanics.Timer;
using SpaceRogue.Enums;
using SpaceRogue.Gameplay.Shooting.Factories;
using SpaceRogue.Gameplay.Shooting.Scriptables;
using UnityEngine;


namespace SpaceRogue.Gameplay.Shooting.Weapons
{
    public sealed class Railgun : Weapon
    {
        #region Fields

        private readonly RailgunConfig _railgunConfig;
        private readonly EntityType _entityType;
        private readonly ProjectileFactory _projectileFactory;

        #endregion

        #region CodeLife

        public Railgun(
            RailgunConfig railgunConfig,
            EntityType entityType,
            ProjectileFactory projectileFactory,
            TimerFactory timerFactory) : base(railgunConfig, timerFactory)
        {
            _railgunConfig = railgunConfig;
            _entityType = entityType;
            _projectileFactory = projectileFactory;
        }

        #endregion

        #region Methods

        public override void CommenceFiring(Vector2 bulletPosition, Quaternion turretRotation)
        {
            if (IsOnCooldown) return;

            _projectileFactory.Create(new ProjectileSpawnParams(bulletPosition, turretRotation, _entityType, _railgunConfig.RailgunProjectile));

            CooldownTimer.Start();
        }

        #endregion
    }
}