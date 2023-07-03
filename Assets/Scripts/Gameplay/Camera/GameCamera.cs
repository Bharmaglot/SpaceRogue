using System;
using Gameplay.Events;
using Gameplay.Services;
using SpaceRogue.Services;
using UnityEngine;


namespace Gameplay.Camera
{
    public sealed class GameCamera : IDisposable
    {
        private readonly Updater _updater;
        private readonly CurrentLevelProgress _gameProgress;
        private readonly Transform _cameraTransform;
        
        private Transform _playerTransform;
        
        private const int CameraZAxisOffset = -10;

        public GameCamera(Updater updater, CurrentLevelProgress gameProgress, CameraView cameraView)
        {
            _updater = updater;
            _gameProgress = gameProgress;
            _cameraTransform = cameraView.transform;
            
            _gameProgress.PlayerSpawned += OnPlayerSpawned;
            _gameProgress.PlayerDestroyed += OnPlayerDestroyed;
        }

        public void Dispose()
        {
            _updater.UnsubscribeFromUpdate(FollowPlayer);
            _gameProgress.PlayerSpawned -= OnPlayerSpawned;
            _gameProgress.PlayerDestroyed -= OnPlayerDestroyed;
        }
        
        private void OnPlayerSpawned(PlayerSpawnedEventArgs eventArgs)
        {
            _playerTransform = eventArgs.Transform;
            _updater.SubscribeToUpdate(FollowPlayer);
        }

        private void OnPlayerDestroyed()
        {
            _updater.UnsubscribeFromUpdate(FollowPlayer);
            _playerTransform = null;
        }

        private void FollowPlayer()
        {
            if(_playerTransform == null)
            {
                return;
            }

            var position = _playerTransform.position;
            _cameraTransform.position = new Vector3(position.x, position.y, position.z + CameraZAxisOffset);
        }
    }
}