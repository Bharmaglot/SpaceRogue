using System;
using Gameplay.Events;
using Gameplay.Player;

namespace Gameplay.GameProgress
{
    public sealed class CurrentLevelProgress : IDisposable
    {
        private readonly Level _level;
        private readonly PlayerFactory _playerFactory;
        
        public event Action<Level> LevelStarted = (_) => { };
        public event Action<int> DefeatedEnemiesCountChange = (_) => { };
        public event Action<PlayerSpawnedEventArgs> PlayerSpawned = (_) => { };
        public event Action PlayerDestroyed = () => { };

        public CurrentLevelProgress(Level level, PlayerFactory playerFactory)
        {
            _level = level;
            _playerFactory = playerFactory;

            _playerFactory.PlayerSpawned += OnPlayerSpawned;
        }

        public void Dispose()
        {
            _playerFactory.PlayerSpawned -= OnPlayerSpawned;
        }

        private void OnPlayerSpawned(PlayerSpawnedEventArgs args)
        {
            PlayerSpawned.Invoke(args);
        }
    }
}