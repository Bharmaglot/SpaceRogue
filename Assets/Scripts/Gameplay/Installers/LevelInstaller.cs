using Gameplay.GameProgress;
using Gameplay.Missions;
using Gameplay.Missions.Factories;
using Gameplay.Missions.Scriptables;
using Gameplay.Services;
using Gameplay.Space;
using Gameplay.Space.Factories;
using Gameplay.Space.Generator;
using Gameplay.Space.Obstacle;
using Gameplay.Space.SpaceObjects.Scriptables;
using Scriptables;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

namespace Gameplay.Installers
{
    public sealed class LevelInstaller : MonoInstaller
    {
        [field: SerializeField] public SpaceView SpaceViewPrefab { get; private set; }
        [field: SerializeField] public LevelPresetsConfig LevelPresetsConfig { get; private set; }

        public override void InstallBindings()
        {
            InstallSpaceView();
            InstallLevel();
            InstallLevelGenerator();
            InstallSpaceObstacle();
            InstallLevelProgressService();
            InstallMissionFactory();
            InstallMissionCompleteCheat();
        }

        private void InstallSpaceView()
        {
            Container
                .Bind<SpaceView>()
                .FromInstance(SpaceViewPrefab)
                .AsSingle();

            Container
                .BindFactory<SpaceView, SpaceViewFactory>()
                .AsSingle();
        }

        private void InstallLevel()
        {
            Container
                .Bind<LevelPresetsConfig>()
                .FromInstance(LevelPresetsConfig)
                .AsSingle();

            Container
                .BindFactory<int, Level, LevelFactory>()
                .AsSingle();
        }

        private void InstallLevelGenerator()
        {
            Container
                .BindFactory<SpaceConfig, MapGenerator, MapGeneratorFactory>()
                .AsSingle();
            
            Container
                .BindFactory<SpaceView, SpaceConfig, int[,], int[,], LevelMap, LevelMapFactory>()
                .AsSingle();
            
            Container
                .BindFactory<int[,], Tilemap, SpawnPointsFinder, SpawnPointsFinderFactory>()
                .AsSingle();
        }
        
        private void InstallSpaceObstacle()
        {
            Container
                .BindFactory<SpaceObstacleView, float, SpaceObstacle, SpaceObstacleFactory>()
                .AsSingle();
        }

        private void InstallLevelProgressService()
        {
            Container
                .BindInterfacesAndSelfTo<LevelProgress>()
                .AsSingle()
                .NonLazy();

            Container
                .BindInterfacesAndSelfTo<LevelCompleteController>()
                .AsSingle()
                .NonLazy();
        }
        
        private void InstallMissionFactory()
        {
            Container
                .BindIFactory<BaseMissionConfig, BaseMission>()
                .FromFactory<MissionFactory>();

            Container
                .BindFactory<int, KillEnemiesMissionConfig, KillEnemiesMission, KillMissionFactory>()
                .AsSingle();
        }
        
        private void InstallMissionCompleteCheat()
        {
            Container
                .BindInterfacesAndSelfTo<InstantMissionCompletionController>()
                .AsSingle();
        }
    }
}