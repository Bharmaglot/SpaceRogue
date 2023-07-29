using SpaceRogue.Scriptables.GameEvent;
using SpaceRogue.Services;
using System;
using UnityEngine;
using Utilities.Unity;


namespace SpaceRogue.Gameplay.GameEvent
{
    public sealed class GameEventIndicator : IDisposable
    {

        #region Fields

        private readonly Collider2D _gameEventObjectCollider;
        private readonly GameEventConfig _gameEventConfig;
        private readonly Updater _updater;
        private readonly GameEventIndicatorView _indicatorView;
        private readonly Camera _camera;

        private bool _isVisible;
        private bool _isVisibleOnce;

        #endregion


        #region CodeLife

        public GameEventIndicator(Updater updater, GameEventIndicatorView indicatorView, Collider2D collider, GameEventConfig gameEventConfig)
        {
            _updater = updater;
            _indicatorView = indicatorView;
            _gameEventObjectCollider = collider;
            _gameEventConfig = gameEventConfig;
            _camera = Camera.main;

            _indicatorView.Hide();
            _indicatorView.Icon.sprite = _gameEventConfig.Icon;
            _indicatorView.IndicatorDiameter.sizeDelta = new(0, _gameEventConfig.IndicatorDiameter);

            _updater.SubscribeToUpdate(RotateToGameEventObject);
        }

        public void Dispose()
        {
            _updater.UnsubscribeFromUpdate(RotateToGameEventObject);

            if (_indicatorView != null)
            {
                UnityEngine.Object.Destroy(_indicatorView.gameObject);
            }
        }

        #endregion


        #region Methods

        private void RotateToGameEventObject()
        {
            if (_gameEventObjectCollider == null)
            {
                Dispose();
                return;
            }

            if (_isVisibleOnce)
            {
                return;
            }

            _isVisible = UnityHelper.IsObjectVisible(_camera, _gameEventObjectCollider.bounds);
            ShowIndicator(_isVisible);

            if (_isVisible)
            {
                if (_gameEventConfig.ShowUntilItIsVisibleOnce)
                {
                    _isVisibleOnce = true;
                }
                return;
            }

            var position = _camera.WorldToScreenPoint(_gameEventObjectCollider.transform.position);
            position = new Vector3(position.x - Screen.width / 2, position.y - Screen.height / 2, 0);
            var angle = Mathf.Atan2(position.x, position.y) * Mathf.Rad2Deg;

            _indicatorView.transform.eulerAngles = Vector3.forward * -angle;
        }

        private void ShowIndicator(bool isVisible)
        {
            if (_isVisibleOnce)
            {
                _indicatorView.Hide();
            }
            else
            {
                if (!isVisible)
                {
                    _indicatorView.Show();
                }
                else
                {
                    _indicatorView.Hide();
                }
            }
        }

        #endregion

    }
}