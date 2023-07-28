using SpaceRogue.Gameplay.GameProgress;
using SpaceRogue.Gameplay.Missions;
using SpaceRogue.Gameplay.Missions.Factories;
using SpaceRogue.Gameplay.Missions.Scriptables;
using Gameplay.Space;
using Gameplay.Space.Factories;
using Gameplay.Space.Generator;
using Gameplay.Space.SpaceObjects.Scriptables;
using Scriptables;
using SpaceRogue.Gameplay.Space.Obstacle;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;


namespace SpaceRogue.Gameplay.Installers
{
    public sealed class LevelInstaller : MonoInstaller
    {

        #region Properties

        [field: SerializeField] public SpaceView SpaceViewPrefab { get; private set; }
        [field: SerializeField] public LevelPresetsConfig LevelPresetsConfig { get; private set; }

        #endregion


        #region Methods

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

        #endregion

    }
}