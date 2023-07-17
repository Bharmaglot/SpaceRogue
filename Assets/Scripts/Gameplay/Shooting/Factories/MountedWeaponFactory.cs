using System;
using Gameplay.Shooting.Scriptables;
using SpaceRogue.Abstraction;
using Zenject;


namespace Gameplay.Shooting.Factories
{
    public class MountedWeaponFactory : IFactory<MountedWeaponConfig, EntityViewBase, MountedWeapon>
    {
        private readonly WeaponFactory _weaponFactory;
        private readonly GunPointViewFactory _gunPointViewFactory;
        private readonly TurretViewFactory _turretViewFactory;
        private readonly TurretMountedWeaponFactory _turretMountedWeaponFactory;

        public MountedWeaponFactory(WeaponFactory weaponFactory, GunPointViewFactory gunPointViewFactory, TurretViewFactory turretViewFactory, TurretMountedWeaponFactory turretMountedWeaponFactory)
        {
            _weaponFactory = weaponFactory;
            _gunPointViewFactory = gunPointViewFactory;
            _turretViewFactory = turretViewFactory;
            _turretMountedWeaponFactory = turretMountedWeaponFactory;
        }

        public MountedWeapon Create(MountedWeaponConfig config, EntityViewBase entityView)
        {
            return config.WeaponMountType switch
            {
                WeaponMountType.None => new UnmountedWeapon(CreateWeapon(config, entityView), entityView),
                WeaponMountType.Frontal => new FrontalMountedWeapon(CreateWeapon(config, entityView), entityView, _gunPointViewFactory),
                WeaponMountType.Turret when config.TurretConfig is not null => _turretMountedWeaponFactory.Create(CreateWeapon(config, entityView), entityView, _turretViewFactory, _gunPointViewFactory, config.TurretConfig),
                WeaponMountType.Turret => new FrontalMountedWeapon(CreateWeapon(config, entityView), entityView, _gunPointViewFactory),
                _ => throw new ArgumentOutOfRangeException(nameof(config), config, "A not-existent weapon mount type is provided")
            };
        }
        
        private Weapon CreateWeapon(MountedWeaponConfig config, EntityViewBase entityView) => _weaponFactory.Create(config.MountedWeapon, entityView.EntityType);
    }
}