using System;
using Gameplay.Events;
using Gameplay.Player;
using UnityEngine;
using Zenject;

namespace Gameplay.GameProgress
{
    public sealed class LevelProgress : IDisposable
    {
        private readonly LevelFactory _levelFactory;
        private readonly PlayerFactory _playerFactory;
        
        public event Action<LevelStartedEventArgs> LevelStarted;
        public event Action<LevelCompletedEventArgs> LevelCompleted;
        public event Action LevelFinished;
        public event Action<PlayerSpawnedEventArgs> PlayerSpawned;
        public event Action<PlayerDestroyedEventArgs> PlayerDestroyed;

        private Level _level;
        private Player.Player _player;

        public LevelProgress(LevelFactory levelFactory, PlayerFactory playerFactory)
        {
            _levelFactory = levelFactory;
            _playerFactory = playerFactory;
            
            _levelFactory.LevelCreated += OnLevelCreated;
            _playerFactory.PlayerSpawned += OnPlayerSpawned;
        }

        public void Dispose()
        {
            _levelFactory.LevelCreated -= OnLevelCreated;
            _playerFactory.PlayerSpawned -= OnPlayerSpawned;
        }

        private void OnLevelCreated(Level level)
        {
            _level = level;
            _level.LevelMission.Completed += OnLevelMissionCompleted;
            
            LevelStarted?.Invoke(
                new LevelStartedEventArgs(
                    _level.CurrentLevelNumber, 
                    _level.LevelMission, 
                    _level.MapCameraSize
                    )
                );
        }

        private void OnLevelMissionCompleted()
        {
            if (_player is null) return;
            
            LevelCompleted?.Invoke(new LevelCompletedEventArgs(_level.CurrentLevelNumber));
            LevelFinished?.Invoke();

            _level.LevelMission.Completed -= OnLevelMissionCompleted;
        }

        private void OnPlayerSpawned(PlayerSpawnedEventArgs args)
        {
            PlayerSpawned?.Invoke(args);

            _player = args.Player;
            _player.PlayerDestroyed += OnPlayerDestroyed;
        }

        private void OnPlayerDestroyed()
        {
            PlayerDestroyed?.Invoke(new PlayerDestroyedEventArgs(_level.CurrentLevelNumber));
            LevelFinished?.Invoke();
            
            _player.PlayerDestroyed -= OnPlayerDestroyed;
            _player = null;
        }
    }
}