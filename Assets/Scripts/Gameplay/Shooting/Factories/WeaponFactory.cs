using Gameplay.Mechanics.Timer;
using Gameplay.Shooting.Scriptables;
using Gameplay.Shooting.Weapons;
using SpaceRogue.Enums;
using System;
using Zenject;


namespace Gameplay.Shooting.Factories
{
    public sealed class WeaponFactory : IFactory<WeaponConfig, EntityType, Weapon>
    {

        #region Fields

        private readonly ProjectileFactory _projectileFactory;
        private readonly TimerFactory _timerFactory;
        private readonly MineFactory _mineFactory;

        #endregion


        #region CodeLife

        public WeaponFactory(ProjectileFactory projectileFactory, MineFactory mineFactory, TimerFactory timerFactory)
        {
            _projectileFactory = projectileFactory;
            _mineFactory = mineFactory;
            _timerFactory = timerFactory;
        }

        #endregion


        #region Methods

        public Weapon Create(WeaponConfig weaponConfig, EntityType entityType)
        {
            return weaponConfig.Type switch
            {
                WeaponType.None => new NullGun(),
                WeaponType.Blaster => new Blaster(weaponConfig as BlasterConfig, entityType, _projectileFactory, _timerFactory),
                WeaponType.Shotgun => new Shotgun(weaponConfig as ShotgunConfig, entityType, _projectileFactory, _timerFactory),
                WeaponType.Minigun => new Minigun(weaponConfig as MinigunConfig, entityType, _projectileFactory, _timerFactory),
                WeaponType.Railgun => new Railgun(weaponConfig as RailgunConfig, entityType, _projectileFactory, _timerFactory),
                WeaponType.Mortar => new Mortar(weaponConfig as MortarConfig, _mineFactory, _timerFactory),
                _ => throw new ArgumentOutOfRangeException(nameof(weaponConfig.Type), weaponConfig.Type,
                    "A not-existent weapon type is provided")
            };
        }

        #endregion

    }
}