using Gameplay.Pooling;
using SpaceRogue.Gameplay.Shooting;
using SpaceRogue.Gameplay.Shooting.Factories;
using SpaceRogue.Gameplay.Shooting.Scriptables;
using SpaceRogue.Abstraction;
using SpaceRogue.Enums;
using UnityEngine;
using Zenject;


namespace Gameplay.Installers
{
    public sealed class WeaponsInstaller : MonoInstaller
    {
        [field: SerializeField] public ProjectilePool ProjectilePool { get; private set; }
        [field: SerializeField] public TurretView TurretView { get; private set; }
        [field: SerializeField] public GunPointView GunPoint { get; private set; }
        
        public override void InstallBindings()
        {
            InstallProjectilePool();
            InstallProjectileFactory();
            InstallTurretFactory();
            InstallGunPointFactory();
            InstallWeaponFactories();
            InstallUnitWeaponFactory();
        }

        private void InstallProjectilePool()
        {
            Container
                .Bind<ProjectilePool>()
                .FromInstance(ProjectilePool)
                .AsSingle();
        }

        private void InstallProjectileFactory()
        {
            Container
                .BindFactory<ProjectileSpawnParams, ProjectileView, ProjectileViewFactory>()
                .AsSingle();

            Container
                .BindFactory<ProjectileSpawnParams, Projectile, ProjectileFactory>()
                .AsSingle();
        }

        private void InstallTurretFactory()
        {
            Container
                .Bind<TurretView>()
                .FromInstance(TurretView)
                .WhenInjectedInto<TurretViewFactory>();

            Container
                .BindFactory<Weapon, EntityViewBase, TurretViewFactory, GunPointViewFactory, TurretConfig, TurretMountedWeapon, TurretMountedWeaponFactory>()
                .AsSingle();

            Container
                .BindFactory<Transform, TurretConfig, TurretView, TurretViewFactory>()
                .AsSingle();
        }
        
        private void InstallGunPointFactory()
        {
            Container
                .Bind<GunPointView>()
                .FromInstance(GunPoint)
                .WhenInjectedInto<GunPointViewFactory>();
            
            Container
                .BindFactory<Vector2, Quaternion, Transform, GunPointView, GunPointViewFactory>()
                .AsSingle();
        }

        private void InstallWeaponFactories()
        {
            Container
                .BindIFactory<WeaponConfig, EntityType, Weapon>()
                .FromFactory<WeaponFactory>();

            Container
                .Bind<WeaponFactory>()
                .AsCached();

            Container
                .BindIFactory<MountedWeaponConfig, EntityViewBase, MountedWeapon>()
                .FromFactory<MountedWeaponFactory>();

            Container
                .Bind<MountedWeaponFactory>()
                .AsCached();
        }
        
        private void InstallUnitWeaponFactory()
        {
            Container
                .BindFactory<EntityViewBase, MountedWeaponConfig, IUnitWeaponInput, UnitWeapon, UnitWeaponFactory>()
                .AsSingle();
        }
    }
}