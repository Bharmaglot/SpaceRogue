using Gameplay.Asteroids;
using Gameplay.Enemy;
using SpaceRogue.Gameplay.GameEvent;
using SpaceRogue.Gameplay.Missions;
using SpaceRogue.Gameplay.Space.Obstacle;
using System;


namespace SpaceRogue.Gameplay.GameProgress
{
    public sealed class Level : IDisposable
    {

        #region Fields

        private readonly EnemyForces _enemyForces;
        private readonly global::Gameplay.Space.Space _space;
        private readonly SpaceObstacle _spaceObstacle;
        private readonly AsteroidsInSpace _asteroids;
        private readonly GameEventsController _gameEventsController;

        #endregion


        #region Properties

        public int CurrentLevelNumber { get; }
        public KillEnemiesMission LevelMission { get; }
        public float MapCameraSize { get; }

        #endregion


        #region CodeLife

        public Level(
            int currentLevelNumber,
            KillEnemiesMission levelMission,
            float mapCameraSize,
            EnemyForces enemyForces,
            global::Gameplay.Space.Space space,
            SpaceObstacle spaceObstacle,
            AsteroidsInSpace asteroids,
            GameEventsController gameEventsController
            )
        {
            CurrentLevelNumber = currentLevelNumber;
            LevelMission = levelMission;
            MapCameraSize = mapCameraSize;

            _enemyForces = enemyForces;
            _space = space;
            _spaceObstacle = spaceObstacle;
            _asteroids = asteroids;
            _gameEventsController = gameEventsController;
        }

        public void Dispose()
        {
            _enemyForces?.Dispose();

            _asteroids.Dispose();
            _spaceObstacle.Dispose();
            _gameEventsController.Dispose();

            _space.Dispose(); //Important to be last!
        }

        #endregion

    }
}