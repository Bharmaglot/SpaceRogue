using SpaceRogue.Gameplay.Background;
using Gameplay.Enemy.Movement;
using SpaceRogue.Gameplay.GameProgress;
using Gameplay.Mechanics.Meter;
using Gameplay.Mechanics.Timer;
using Gameplay.Movement;
using Gameplay.Services;
using Scriptables;
using SpaceRogue.Abstraction;
using SpaceRogue.Enemy.Movement;
using SpaceRogue.InputSystem;
using SpaceRogue.Player.Movement;
using UnityEngine;
using Zenject;


namespace Gameplay.Installers
{
    public sealed class GameplayServicesInstaller : MonoInstaller
    {
        [field: SerializeField] public GameBackgroundConfig GameBackgroundConfig { get; private set; }
        [field: SerializeField] public GameBackgroundView GameBackgroundView { get; private set; }

        [field: SerializeField] public PlayerInputConfig PlayerInputConfig { get; private set; }

        public override void InstallBindings()
        {
            InstallGameplayMechanics();
            InstallGameProgressState();
            InstallBackground();
            InstallPlayerInput();
            InstallEnemyInput();
            InstallUnitMovement();
            InstallPlayerLocator();
            InstallEnemiesAlarm();
            InstallEnemyDeathObserver();
        }

        private void InstallGameplayMechanics()
        {
            Container
                .BindFactory<float, Timer, TimerFactory>()
                .AsSingle();

            Container
                .BindFactory<float, float, float, MeterWithCooldown, MeterWithCooldownFactory>()
                .AsSingle();
        }

        private void InstallGameProgressState()
        {
            Container
                .BindInterfacesAndSelfTo<GameProgressState>()
                .AsSingle()
                .NonLazy();
        }

        private void InstallBackground()
        {
            Container
                .Bind<GameBackgroundConfig>()
                .FromInstance(GameBackgroundConfig)
                .AsSingle()
                .NonLazy();

            Container
                .Bind<GameBackgroundView>()
                .FromInstance(GameBackgroundView)
                .AsSingle()
                .NonLazy();

            Container
                .BindInterfacesAndSelfTo<GameBackground>()
                .AsSingle()
                .NonLazy();
        }

        private void InstallPlayerInput()
        {
            Container
                .Bind<PlayerInputConfig>()
                .FromInstance(PlayerInputConfig)
                .AsSingle()
                .NonLazy();
            
            Container
                .BindInterfacesAndSelfTo<PlayerInput>()
                .AsSingle()
                .NonLazy();
        }

        private void InstallEnemyInput()
        {
            Container
                .BindFactory<EnemyInput, EnemyInputFactory>()
                .AsSingle();
        }

        private void InstallUnitMovement()
        {
            Container
                .BindFactory<UnitMovementConfig, UnitMovementModel, UnitMovementModelFactory>()
                .AsSingle();

            Container
                .BindFactory<EntityViewBase, IUnitMovementInput, UnitMovementModel, UnitMovement, UnitMovementFactory>()
                .AsSingle();

            Container
                .BindFactory<EntityViewBase, IUnitTurningInput, UnitMovementModel, UnitTurning, UnitTurningFactory>()
                .AsSingle();
            
            Container
                .BindFactory<EntityViewBase, IUnitTurningMouseInput, UnitMovementModel, UnitTurningMouse, UnitTurningMouseFactory>()
                .AsSingle();
        }
        
        private void InstallPlayerLocator()
        {
            Container
                .BindInterfacesAndSelfTo<PlayerLocator>()
                .AsSingle()
                .NonLazy();
        }
        
        private void InstallEnemiesAlarm()
        {
            Container
                .Bind<EnemiesAlarm>()
                .AsSingle()
                .NonLazy();
        }

        private void InstallEnemyDeathObserver()
        {
            Container
                .BindInterfacesAndSelfTo<EnemyDeathObserver>()
                .AsSingle()
                .NonLazy();
        }
    }
}