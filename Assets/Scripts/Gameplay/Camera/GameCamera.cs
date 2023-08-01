using SpaceRogue.Gameplay.Events;
using SpaceRogue.Gameplay.GameProgress;
using SpaceRogue.Services;
using System;
using UnityEngine;


namespace SpaceRogue.Gameplay.Camera
{
    public sealed class GameCamera : IDisposable
    {

        #region Fields

        private const int CAMERA_Z_AXIS_OFFSET = -10;

        private readonly Updater _updater;
        private readonly LevelProgress _gameProgress;
        private readonly Transform _cameraTransform;

        private Transform _playerTransform;

        #endregion


        #region CodeLife

        public GameCamera(Updater updater, LevelProgress gameProgress, CameraView cameraView)
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

        #endregion


        #region Methods

        private void OnPlayerSpawned(PlayerSpawnedEventArgs eventArgs)
        {
            _playerTransform = eventArgs.Transform;
            _updater.SubscribeToUpdate(FollowPlayer);
        }

        private void OnPlayerDestroyed(PlayerDestroyedEventArgs _)
        {
            _updater.UnsubscribeFromUpdate(FollowPlayer);
            _playerTransform = null;
        }

        private void FollowPlayer()
        {
            if (_playerTransform == null)
            {
                return;
            }

            var position = _playerTransform.position;
            _cameraTransform.position = new Vector3(position.x, position.y, position.z + CAMERA_Z_AXIS_OFFSET);
        }

        #endregion

    }
}