using System;
using Abstracts;
using Gameplay.Abstracts;
using Gameplay.Shooting.Scriptables;
using Services;
using Zenject;

namespace Gameplay.Shooting.Factories
{
    public class MountedWeaponFactory : IFactory<MountedWeaponConfig, EntityView, MountedWeapon>
    {
        private readonly IFactory<WeaponConfig, EntityType, Weapon> _weaponFactory;
        private readonly GunPointViewFactory _gunPointViewFactory;
        private readonly TurretViewFactory _turretViewFactory;
        private readonly Updater _updater;

        public MountedWeaponFactory(IFactory<WeaponConfig, EntityType, Weapon> weaponFactory, GunPointViewFactory gunPointViewFactory, TurretViewFactory turretViewFactory, Updater updater)
        {
            _weaponFactory = weaponFactory;
            _gunPointViewFactory = gunPointViewFactory;
            _turretViewFactory = turretViewFactory;
            _updater = updater;
        }

        public MountedWeapon Create(MountedWeaponConfig config, EntityView entityView)
        {
            return config.WeaponMountType switch
            {
                WeaponMountType.None => new UnmountedWeapon(CreateWeapon(config, entityView), entityView),
                WeaponMountType.Frontal => new FrontalMountedWeapon(CreateWeapon(config, entityView), entityView, _gunPointViewFactory),
                WeaponMountType.Turret when config.TurretConfig is not null => new TurretMountedWeapon(CreateWeapon(config, entityView), entityView, _turretViewFactory, _gunPointViewFactory, config.TurretConfig, _updater),
                WeaponMountType.Turret => new FrontalMountedWeapon(CreateWeapon(config, entityView), entityView, _gunPointViewFactory),
                _ => throw new ArgumentOutOfRangeException(nameof(config), config, "A not-existent weapon mount type is provided")
            };
        }
        
        private Weapon CreateWeapon(MountedWeaponConfig config, EntityView entityView) => _weaponFactory.Create(config.MountedWeapon, entityView.EntityType);
    }
}