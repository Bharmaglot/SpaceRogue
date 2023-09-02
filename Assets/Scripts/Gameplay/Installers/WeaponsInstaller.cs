using Gameplay.Pooling;
using SpaceRogue.Abstraction;
using SpaceRogue.Enums;
using SpaceRogue.Gameplay.Shooting;
using SpaceRogue.Gameplay.Shooting.Factories;
using SpaceRogue.Gameplay.Shooting.Scriptables;
using SpaceRogue.Player.Movement;
using UnityEngine;
using Zenject;


namespace SpaceRogue.Gameplay.Installers
{
    public sealed class WeaponsInstaller : MonoInstaller
    {

        #region Properties

        [field: SerializeField] public ProjectilePool ProjectilePool { get; private set; }
        [field: SerializeField] public TurretView TurretView { get; private set; }
        [field: SerializeField] public GunPointView GunPoint { get; private set; }

        #endregion


        #region Methods

        public override void InstallBindings()
        {
            InstallProjectilePool();
            InstallProjectileFactory();
            InstallMineFactory();
            InstallWaveExplosion();
            InstallInstantExplosion();
            InstallTurretFactory();
            InstallRocketFactory();
            InstallTorpedoFactory();
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

        private void InstallMineFactory()
        {
            Container
                .BindFactory<Vector2, MineConfig, MineView, MineViewFactory>()
                .AsSingle();

            Container
                .BindFactory<Vector2, MineConfig, IDestroyable, Mine, MineFactory>()
                .AsSingle();
        }

        private void InstallRocketFactory()
        {
            Container
                .BindFactory<Vector2, Quaternion, BaseReactiveObjectConfig, ReactiveObjectView, RocketViewFactory>()
                .AsSingle();

            Container
                .BindFactory<Vector2, Quaternion, RocketConfig, InstantExplosionFactory, IDestroyable, Rocket, RocketFactory>()
                .AsSingle();
        }

        private void InstallTorpedoFactory()
        {
            Container
                .BindFactory<Vector2, Quaternion, TorpedoConfig, InstantExplosionFactory, IDestroyable, Torpedo, TorpedoFactory>()
                .AsSingle();
        }

        private void InstallWaveExplosion()
        {
            Container
                .BindFactory<Vector2, BaseExplosionObjectConfig, ExplosionView, ExplosionViewFactory>()
               .AsSingle();

            Container
                .BindFactory<Vector2, WaveExplosionConfig, IDestroyable, WaveExplosion, WaveExplosionFactory>()
               .AsSingle();
        }

        private void InstallInstantExplosion()
        {
            Container
                .BindFactory<Vector2, InstantExplosionConfig, IDestroyable, InstantExplosion, InstantExplosionFactory>()
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
                .BindFactory<EntityViewBase, MountedWeaponConfig, UnitMovement, IUnitWeaponInput, UnitWeapon, UnitWeaponFactory>()
                .AsSingle();
        }

        #endregion

    }
}