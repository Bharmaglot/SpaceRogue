using System;
using Gameplay.Asteroids;
using Gameplay.Enemy;
using Gameplay.Missions;
using Gameplay.Space.Obstacle;

namespace Gameplay.GameProgress
{
    public sealed class Level : IDisposable
    {
        private readonly Player.Player _player;
        private readonly EnemyForces _enemyForces;
        private readonly Space.Space _space;
        private readonly SpaceObstacle _spaceObstacle;
        private readonly AsteroidsInSpace _asteroids;

        public int CurrentLevelNumber { get; private set; }
        public KillEnemiesMission LevelMission { get; private set; }
        public float MapCameraSize { get; private set; }

        public Level(
            int currentLevelNumber,
            KillEnemiesMission levelMission,
            float mapCameraSize,
            Player.Player player,
            EnemyForces enemyForces,
            Space.Space space,
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
            _spaceObstacle.Dispose();
            _space.Dispose();
            _enemyForces.Dispose();
            _asteroids.Dispose();
            _player.Dispose();
        }
    }
}