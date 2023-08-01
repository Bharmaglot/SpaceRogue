using Gameplay.Mechanics.Timer;
using SpaceRogue.Gameplay.Events;
using SpaceRogue.Gameplay.Player;
using System;
using UnityEngine;


namespace Gameplay.Services
{
    public sealed class PlayerLocator : IDisposable
    {

        #region Events

        public event Action<Transform> PlayerTransform;

        #endregion


        #region Fields

        private const float LOCATOR_COOLDOWN = 1.0f;

        private readonly Timer _timer;
        private readonly PlayerFactory _playerFactory;

        private Transform _playerTransform;
        private Player _player;

        #endregion


        #region CodeLife

        public PlayerLocator(TimerFactory timerFactory, PlayerFactory playerFactory)
        {
            _timer = timerFactory.Create(LOCATOR_COOLDOWN);
            _playerFactory = playerFactory;

            _playerFactory.PlayerSpawned += OnPlayerSpawned;
        }

        public void Dispose()
        {
            _timer.OnExpire -= Locator;
            _playerFactory.PlayerSpawned -= OnPlayerSpawned;
        }

        #endregion


        #region Methods

        private void Locator()
        {
            if (_playerTransform == null)
            {
                Dispose();
                return;
            }

            _timer.Start();
            PlayerTransform?.Invoke(_playerTransform);
        }

        private void OnPlayerSpawned(PlayerSpawnedEventArgs args)
        {
            _playerTransform = args.Transform;
            _player = args.Player;
            _player.PlayerDisposed += OnPlayerDisposed;
            _timer.Start();
            _timer.OnExpire += Locator;
        }

        private void OnPlayerDisposed()
        {
            _timer.OnExpire -= Locator;
            _player.PlayerDisposed -= OnPlayerDisposed;
            _player = null;
        }

        #endregion

    }
}