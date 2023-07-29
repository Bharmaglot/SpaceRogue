using Gameplay.Pooling;
using Gameplay.Space.Factories;
using Gameplay.Space.SpaceObjects.Scriptables;
using Gameplay.Space.SpaceObjects.SpaceObjectsEffects;
using Gameplay.Space.SpaceObjects.SpaceObjectsEffects.Views;
using SpaceRogue.Abstraction;
using SpaceRogue.Enums;
using SpaceRogue.Gameplay.Abilities;
using SpaceRogue.Gameplay.Abilities.Scriptables;
using SpaceRogue.Gameplay.Pooling;
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
        [field: SerializeField] public AbilityPool AbilityPool { get; private set; }
        [field: SerializeField] public TurretView TurretView { get; private set; }
        [field: SerializeField] public GunPointView GunPoint { get; private set; }

        #endregion


        #region Methods

        public override void InstallBindings()
        {
            InstallProjectilePool();
            InstallProjectileFactory();
            InstallMineFactory();
            InstallTurretFactory();
            InstallGunPointFactory();
            InstallWeaponFactories();
            InstallUnitWeaponFactory();
            InstallAbilityFactories();
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

        private void InstallAbilityFactories()
        {

            Container
                .Bind<AbilityPool>()
                .FromInstance(AbilityPool)
                .AsSingle();

            Container
                .BindIFactory<AbilityConfig, EntityViewBase, UnitMovement, Ability>()
                .FromFactory<AbilityFactory>();

            Container
                .Bind<AbilityFactory>()
                .AsCached();

            Container
                .BindFactory<Vector2, AbilityConfig, AbilityView, AbilityViewFactory>()
                .AsSingle();

            Container
                .BindFactory<AbilityView, Transform, ShotgunAbilityConfig, GravitationMine, GravitationMineFactory>()
                .AsSingle();
        }


        #endregion

    }
}