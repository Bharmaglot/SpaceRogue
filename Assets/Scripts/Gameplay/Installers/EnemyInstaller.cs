using Gameplay.Enemy.Scriptables;
using Gameplay.Enemy;
using Gameplay.Space.Generator;
using UnityEngine;
using Zenject;
using Gameplay.Pooling;
using Gameplay.Movement;
using Gameplay.Enemy.Behaviour;
using SpaceRogue.Enemy.Movement;


namespace Gameplay.Installers
{
    public sealed class EnemyInstaller : MonoInstaller
    {
        [field: SerializeField] public EnemyPool EnemyPool { get; private set; }
        [field: SerializeField] public EnemySpawnConfig EnemySpawnConfig{ get; private set; }

        public override void InstallBindings()
        {
            InstallEnemyPool();
            InstallEnemyView();
            InstallEnemiesGroup();
            InstallEnemyForces();
            InstallEnemyBehaviourSwitcherFactory();
            InstallEnemy();
        }

        private void InstallEnemyPool()
        {
            Container
                .Bind<EnemyPool>()
                .FromInstance(EnemyPool)
                .AsSingle();
        }

        private void InstallEnemyView()
        {
            Container
                .BindFactory<Vector2, EnemyConfig, EnemyView, EnemyViewFactory>()
                .AsSingle();
        }

        private void InstallEnemiesGroup()
        {
            Container
                .BindFactory<EnemyGroupConfig, Vector2, EnemiesGroup, EnemiesGroupFactory>()
                .AsSingle();
        }

        private void InstallEnemyForces()
        {
            Container
                .Bind<EnemySpawnConfig>()
                .FromInstance(EnemySpawnConfig)
                .WhenInjectedInto<EnemyForcesFactory>();

            Container
                .BindFactory<int, SpawnPointsFinder, EnemyForces, EnemyForcesFactory>()
                .AsSingle();
        }
        
        private void InstallEnemyBehaviourSwitcherFactory()
        {
            Container
                .BindIFactory<EnemyStateInfo, EnemyView, EnemyInput, UnitMovementModel, EnemyBehaviourConfig, Transform, EnemyBehaviour>()
                .FromFactory<EnemyBehaviourFactory>();

            Container
                .Bind<EnemyBehaviourFactory>()
                .AsCached();

            Container
                .BindFactory<EnemyView, EnemyInput, UnitMovementModel, EnemyBehaviourConfig, EnemyBehaviourSwitcher, EnemyBehaviourSwitcherFactory>()
                .AsSingle();
        }
        
        private void InstallEnemy()
        {
            Container
                .BindFactory<Vector2, EnemyConfig, Enemy.Enemy, EnemyFactory>()
                .AsSingle();
        }
    }
}