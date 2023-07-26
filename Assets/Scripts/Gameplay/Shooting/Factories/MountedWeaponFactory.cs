using SpaceRogue.Abstraction;
using SpaceRogue.Enums;
using SpaceRogue.Gameplay.Shooting.Scriptables;
using System;
using Zenject;


namespace SpaceRogue.Gameplay.Shooting.Factories
{
    public sealed class MountedWeaponFactory : IFactory<MountedWeaponConfig, EntityViewBase, MountedWeapon>
    {

        #region Fields

        private readonly WeaponFactory _weaponFactory;
        private readonly GunPointViewFactory _gunPointViewFactory;
        private readonly TurretViewFactory _turretViewFactory;
        private readonly TurretMountedWeaponFactory _turretMountedWeaponFactory;

        #endregion


        #region CodeLife

        public MountedWeaponFactory(
            WeaponFactory weaponFactory,
            GunPointViewFactory gunPointViewFactory,
            TurretViewFactory turretViewFactory,
            TurretMountedWeaponFactory turretMountedWeaponFactory)
        {
            _weaponFactory = weaponFactory;
            _gunPointViewFactory = gunPointViewFactory;
            _turretViewFactory = turretViewFactory;
            _turretMountedWeaponFactory = turretMountedWeaponFactory;
        }

        #endregion


        #region Methods

        public MountedWeapon Create(MountedWeaponConfig config, EntityViewBase entityView) => config.WeaponMountType switch
        {
            WeaponMountType.None => new UnmountedWeapon(CreateWeapon(config, entityView), entityView),
            WeaponMountType.Frontal => new FrontalMountedWeapon(CreateWeapon(config, entityView), entityView, _gunPointViewFactory),
            WeaponMountType.Turret when config.TurretConfig != null 
            => _turretMountedWeaponFactory.Create(CreateWeapon(config, entityView), entityView, _turretViewFactory, _gunPointViewFactory, config.TurretConfig),
            WeaponMountType.Turret => new FrontalMountedWeapon(CreateWeapon(config, entityView), entityView, _gunPointViewFactory),
            _ => throw new ArgumentOutOfRangeException(nameof(config), config, $"A not-existent weapon mount type is provided")
        };

        private Weapon CreateWeapon(MountedWeaponConfig config, EntityViewBase entityView) => _weaponFactory.Create(config.MountedWeapon, entityView.EntityType);

        #endregion

    }
}