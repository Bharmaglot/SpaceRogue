using System;
using Gameplay.Asteroids;
using Gameplay.Enemy;
using Gameplay.Missions;

namespace Gameplay.GameProgress
{
    public sealed class Level : IDisposable
    {
        private readonly Player.Player _player;
        private readonly EnemyForces _enemyForces;
        private readonly Space.Space _space;
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
            AsteroidsInSpace asteroids)
        {
            CurrentLevelNumber = currentLevelNumber;
            LevelMission = levelMission;
            MapCameraSize = mapCameraSize;
            
            _player = player;
            _enemyForces = enemyForces;
            _space = space;
            _asteroids = asteroids;
        }

        public void Dispose()
        {
            _player.Dispose();
            _enemyForces.Dispose();
            _space.Dispose();
            _asteroids.Dispose();
        }
    }
}