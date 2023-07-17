using System;
using Gameplay.Events;
using Gameplay.Mechanics.Timer;
using Gameplay.Player;
using UnityEngine;

namespace Gameplay.Services
{
    public sealed class PlayerLocator : IDisposable
    {
        private const float LocatorCooldown = 1;

        private readonly Timer _timer;
        private readonly PlayerFactory _playerFactory;

        private Transform _playerTransform;

        public event Action<Transform> PlayerTransform = _ => { };

        public PlayerLocator(TimerFactory timerFactory, PlayerFactory playerFactory)
        {
            _timer = timerFactory.Create(LocatorCooldown);
            _playerFactory = playerFactory;

            _playerFactory.PlayerSpawned += OnPlayerSpawned;
        }

        public void Dispose()
        {
            _timer.OnExpire -= Locator;
            _playerFactory.PlayerSpawned -= OnPlayerSpawned;
        }

        private void Locator()
        {
            if(_playerTransform == null)
            {
                Dispose();
                return;
            }

            _timer.Start();
            PlayerTransform.Invoke(_playerTransform);
        }

        private void OnPlayerSpawned(PlayerSpawnedEventArgs args)
        {
            _playerTransform = args.Transform;
            _timer.Start();
            _timer.OnExpire += Locator;
        }
    }
}