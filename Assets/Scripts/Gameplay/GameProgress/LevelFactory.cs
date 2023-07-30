using Gameplay.Asteroids.Factories;
using Gameplay.Enemy;
using Gameplay.Space.Factories;
using Gameplay.Space.Generator;
using Scriptables;
using SpaceRogue.Gameplay.GameEvent.Factories;
using SpaceRogue.Gameplay.Missions.Factories;
using SpaceRogue.Gameplay.Player;
using SpaceRogue.Gameplay.Space.Obstacle;
using System;
using Zenject;


namespace SpaceRogue.Gameplay.GameProgress
{
    public sealed class LevelFactory : PlaceholderFactory<Player.Player, int, Level>
    {

        #region Events

        public event Action<Level> LevelCreated;

        #endregion


        #region Fields

        private readonly LevelPresetsConfig _levelPresetsConfig;
        private readonly SpaceViewFactory _spaceViewFactory;
        private readonly MapGeneratorFactory _mapGeneratorFactory;
        private readonly LevelMapFactory _levelMapFactory;
        private readonly SpawnPointsFinderFactory _spawnPointsFinderFactory;
        private readonly SpaceObstacleFactory _spaceObstacleFactory;
        private readonly PlayerFactory _playerFactory;
        private readonly SpaceFactory _spaceFactory;
        private readonly EnemyForcesFactory _enemyForcesFactory;
        private readonly AsteroidsInSpaceFactory _asteroidsInSpaceFactory;
        private readonly KillMissionFactory _missionFactory;
        private readonly GameEventsControllerFactory _gameEventsControllerFactory;
        private readonly GameEventFactory _gameEventFactory;
        private LevelPreset _currentLevelPreset;

        #endregion


        #region CodeLife

        public LevelFactory(
            LevelPresetsConfig levelPresetsConfig,
            SpaceViewFactory spaceViewFactory,
            MapGeneratorFactory mapGeneratorFactory,
            LevelMapFactory levelMapFactory,
            SpawnPointsFinderFactory spawnPointsFinderFactory,
            SpaceObstacleFactory spaceObstacleFactory,
            PlayerFactory playerFactory,
            SpaceFactory spaceFactory,
            EnemyForcesFactory enemyForcesFactory,
            AsteroidsInSpaceFactory asteroidsInSpaceFactory,
            KillMissionFactory missionFactory,
            GameEventsControllerFactory gameEventsControllerFactory,
            GameEventFactory gameEventFactory)
        {
            _levelPresetsConfig = levelPresetsConfig;
            _spaceViewFactory = spaceViewFactory;
            _mapGeneratorFactory = mapGeneratorFactory;
            _levelMapFactory = levelMapFactory;
            _spawnPointsFinderFactory = spawnPointsFinderFactory;
            _spaceObstacleFactory = spaceObstacleFactory;
            _playerFactory = playerFactory;
            _spaceFactory = spaceFactory;
            _enemyForcesFactory = enemyForcesFactory;
            _asteroidsInSpaceFactory = asteroidsInSpaceFactory;
            _missionFactory = missionFactory;
            _gameEventsControllerFactory = gameEventsControllerFactory;
            _gameEventFactory = gameEventFactory;
        }

        #endregion


        #region Methods

        public override Level Create(Player.Player player, int levelNumber)
        {
            _currentLevelPreset = PickRandomLevelPreset();
            var spaceView = _spaceViewFactory.Create();

            var map = _mapGeneratorFactory.Create(_currentLevelPreset.SpaceConfig);
            map.Generate();

            var levelMap = _levelMapFactory.Create(spaceView, _currentLevelPreset.SpaceConfig, map.BorderMap, map.NebulaMap);
            levelMap.Draw();
            var mapCameraSize = levelMap.GetMapCameraSize();

            var spawnPointsFinder = _spawnPointsFinderFactory.Create(map.NebulaMap, spaceView.NebulaTilemap);

            var obstacle = _spaceObstacleFactory.Create(spaceView.SpaceObstacleView, _currentLevelPreset.SpaceConfig.ObstacleForce);

            player.SetStartPosition(spawnPointsFinder.GetPlayerSpawnPoint());

            var space = _spaceFactory.Create(_currentLevelPreset.SpaceConfig.SpaceObjectCount, spaceView, spawnPointsFinder);

            var enemyForces = _enemyForcesFactory.Create(_currentLevelPreset.SpaceConfig.EnemyGroupCount, spawnPointsFinder);

            var asteroids = _asteroidsInSpaceFactory.Create(_currentLevelPreset.SpaceConfig.AsteroidsOnStartCount, spawnPointsFinder);
            asteroids.SpawnStartAsteroids();

            var enemiesSpawned = enemyForces.GetEnemiesCount();
            var mission = _missionFactory.Create(enemiesSpawned, _currentLevelPreset.LevelMission);

            var gameEventsController = _gameEventsControllerFactory.Create(_currentLevelPreset.GameEventsConfig, _gameEventFactory, player.PlayerView);

            var level = new Level(levelNumber, mission, mapCameraSize, enemyForces, space, obstacle, asteroids, gameEventsController);
            LevelCreated?.Invoke(level);
            return level;
        }

        private LevelPreset PickRandomLevelPreset()
        {
            var index = new Random().Next(_levelPresetsConfig.Presets.Count);
            return _levelPresetsConfig.Presets[index];
        }

        #endregion

    }
}