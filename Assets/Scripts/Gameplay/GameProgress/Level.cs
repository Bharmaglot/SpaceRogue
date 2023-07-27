using System;
using Gameplay.Asteroids;
using Gameplay.Enemy;
using Gameplay.Missions;
using SpaceRogue.Gameplay.Space.Obstacle;

namespace Gameplay.GameProgress
{
    public sealed class Level : IDisposable
    {
        private readonly Player.Player _player;
        private readonly EnemyForces _enemyForces;
        private readonly Space.Space _space;
        private readonly SpaceObstacle _spaceObstacle;
        private readonly AsteroidsInSpace _asteroids;

        public int CurrentLevelNumber { get; }
        public KillEnemiesMission LevelMission { get; }
        public float MapCameraSize { get; }

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
            if (_enemyForces is not null)
            {
                _enemyForces.Dispose();
            }

            if (_player is not null)
            {
                _player.Dispose();
            }

            _asteroids.Dispose();
            _spaceObstacle.Dispose();
            
            
            
            _space.Dispose(); //Important to be last!
        }
    }
}