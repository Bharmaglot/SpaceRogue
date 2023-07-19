using System;
using Gameplay.Events;
using Gameplay.Player;

namespace Gameplay.GameProgress
{
    public sealed class LevelProgress : IDisposable
    {
        //private readonly Level _level;
        private readonly PlayerFactory _playerFactory;
        
        public event Action<Level> LevelStarted = _ => { };
        public event Action<int> DefeatedEnemiesCountChange = _ => { };
        public event Action<PlayerSpawnedEventArgs> PlayerSpawned = _ => { };
        public event Action PlayerDestroyed = () => { };

        private Player.Player _player;

        public LevelProgress(/*Level level,*/ PlayerFactory playerFactory)
        {
            //_level = level;
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

            _player = args.Player;
            _player.PlayerDestroyed += OnPlayerDestroyed;
        }

        private void OnPlayerDestroyed()
        {
            PlayerDestroyed.Invoke();
            _player.PlayerDestroyed -= OnPlayerDestroyed;
            _player = null;
        }
    }
}