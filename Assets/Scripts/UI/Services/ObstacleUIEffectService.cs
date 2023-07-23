using SpaceRogue.Gameplay.Space.Obstacle;
using SpaceRogue.Services;
using SpaceRogue.UI.Game;
using System;

namespace SpaceRogue.UI.Services
{
    public sealed class ObstacleUIEffectService : IDisposable
    {
        #region Fields

        private readonly Updater _updater;
        private readonly ObstacleUIEffectView _obstacleUIEffectView;
        private readonly SpaceObstacleFactory _spaceObstacleFactory;

        private SpaceObstacle _spaceObstacle;

        private bool _isIncrease;
        private bool _isBlocked;

        #endregion

        #region CodeLife

        public ObstacleUIEffectService(Updater updater, ObstacleUIEffectView obstacleUIEffectView, SpaceObstacleFactory spaceObstacleFactory)
        {
            _updater = updater;
            _obstacleUIEffectView = obstacleUIEffectView;
            _spaceObstacleFactory = spaceObstacleFactory;

            _obstacleUIEffectView.SetVignetteSettings();
            _obstacleUIEffectView.VignetteSizeLimit += BlockUpdate;
            _spaceObstacleFactory.SpaceObstacleCreated += OnSpaceObstacleCreated;
        }

        public void Dispose()
        {
            _obstacleUIEffectView.VignetteSizeLimit -= BlockUpdate;
            _spaceObstacleFactory.SpaceObstacleCreated -= OnSpaceObstacleCreated;

            if (_spaceObstacle == null) return;
            UnsubscribeFromObstacleEffect();
        }

        #endregion

        #region Methods

        private void BlockUpdate() => _isBlocked = true;

        private void OnSpaceObstacleCreated(SpaceObstacle spaceObstacle)
        {
            if (_spaceObstacle != null) UnsubscribeFromObstacleEffect();

            _spaceObstacle = spaceObstacle;
            _spaceObstacle.PlayerInObstacle += OnPlayerInObstacle;
            _spaceObstacle.PlayerOutObstacle += OnPlayerOutObstacle;

            _updater.SubscribeToUpdate(ChangeVignetteSize);
            _isBlocked = true;
        }

        private void UnsubscribeFromObstacleEffect()
        {
            _spaceObstacle.PlayerInObstacle -= OnPlayerInObstacle;
            _spaceObstacle.PlayerOutObstacle -= OnPlayerOutObstacle;

            _updater.UnsubscribeFromUpdate(ChangeVignetteSize);
        }

        private void OnPlayerInObstacle()
        {
            _isIncrease = true;
            _isBlocked = false;
        }

        private void OnPlayerOutObstacle()
        {
            _isIncrease = false;
            _isBlocked = false;
        }

        private void ChangeVignetteSize()
        {
            if (!_isBlocked) _obstacleUIEffectView.ChangeVignetteSize(_isIncrease);
        }

        #endregion
    }
}