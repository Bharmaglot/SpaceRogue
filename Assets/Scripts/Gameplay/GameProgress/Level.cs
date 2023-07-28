using Gameplay.Asteroids;
using Gameplay.Enemy;
using SpaceRogue.Gameplay.Missions;
using SpaceRogue.Gameplay.Space.Obstacle;
using System;


namespace SpaceRogue.Gameplay.GameProgress
{
    public sealed class Level : IDisposable
    {

        #region Fields

        private readonly global::Gameplay.Player.Player _player;
        private readonly EnemyForces _enemyForces;
        private readonly global::Gameplay.Space.Space _space;
        private readonly SpaceObstacle _spaceObstacle;
        private readonly AsteroidsInSpace _asteroids;

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
            global::Gameplay.Player.Player player,
            EnemyForces enemyForces,
            global::Gameplay.Space.Space space,
            SpaceObstacle spaceObstacle,
            AsteroidsInSpace asteroids
            )
        {
            CurrentLevelNumber = currentLevelNumber;
            LevelMission = levelMission;
            MapCameraSize = mapCameraSize;

            _player = player;
            _enemyForces = enemyForces;
            _space = space;
            _spaceObstacle = spaceObstacle;
            _asteroids = asteroids;
        }

        public void Dispose()
        {
            _enemyForces?.Dispose();
            _player?.Dispose();

            _asteroids.Dispose();
            _spaceObstacle.Dispose();

            _space.Dispose(); //Important to be last!
        }

        #endregion

    }
}