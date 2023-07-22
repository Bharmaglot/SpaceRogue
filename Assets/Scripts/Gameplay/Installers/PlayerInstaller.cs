using Gameplay.Movement;
using Gameplay.Player;
using SpaceRogue.Gameplay.Shooting;
using SpaceRogue.Gameplay.Shooting.Scriptables;
using Gameplay.Survival;
using Scriptables;
using SpaceRogue.Abstraction;
using SpaceRogue.Player.Movement;
using UnityEngine;
using Zenject;


namespace Gameplay.Installers
{
    public sealed class PlayerInstaller : MonoInstaller
    {
        [field: SerializeField] public PlayerView PlayerViewPrefab { get; private set; }
        [field: SerializeField] public PlayerConfig PlayerConfig { get; private set; }
        
        public override void InstallBindings()
        {
            InstallPlayerView();
            InstallPlayerMovement();
            InstallPlayerHealth();
            InstallPlayerWeapon();
            InstallPlayer();
        }

        private void InstallPlayerView()
        {
            Container
                .Bind<PlayerView>()
                .FromInstance(PlayerViewPrefab)
                .WhenInjectedInto<PlayerViewFactory>();

            Container
                .BindFactory<Vector2, PlayerView, PlayerViewFactory>()
                .AsSingle();
        }

        private void InstallPlayerMovement()
        {
            Container
                .Bind<UnitMovementConfig>()
                .FromInstance(PlayerConfig.UnitMovement)
                .WhenInjectedInto<PlayerFactory>();

            Container
                .BindFactory<PlayerView, IUnitMovementInput, UnitMovementModel, UnitMovement, PlayerMovementFactory>()
                .AsSingle();
        }

        private void InstallPlayerHealth()
        {
            Container.Bind<EntitySurvivalConfig>()
                .FromInstance(PlayerConfig.Survival)
                .WhenInjectedInto<PlayerSurvivalFactory>();
            
            Container
                .BindFactory<EntityViewBase, EntitySurvival, PlayerSurvivalFactory>()
                .AsSingle();
        }

        private void InstallPlayerWeapon()
        {
            Container
                .Bind<MountedWeaponConfig>()
                .FromInstance(PlayerConfig.StartingWeapon)
                .WhenInjectedInto<PlayerWeaponFactory>();

            Container
                .BindFactory<PlayerView, UnitMovement, UnitWeapon, PlayerWeaponFactory>()
                .AsSingle();
        }

        private void InstallPlayer()
        {
            Container
                .BindFactory<Vector2, Player.Player, PlayerFactory>()
                .AsSingle();
        }
    }
}