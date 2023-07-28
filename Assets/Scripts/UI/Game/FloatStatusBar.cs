using Gameplay.Camera;
using Gameplay.Survival;
using SpaceRogue.Services;
using System;
using UnityEngine;
using Utilities.Unity;


namespace UI.Game
{
    public sealed class FloatStatusBar : IDisposable
    {
        private const float HealthBarOffset = 5f;

        private readonly Updater _updater;
        private readonly Camera _mainCamera;
        private readonly HealthStatusBarView _statusBarView;
        private readonly Collider2D _unitCollider;
        private readonly EntitySurvival _entitySurvival;

        private readonly HealthShieldStatusBarView _shieldStatusBarView;
        private readonly Vector2 _referenceResolution;


        public FloatStatusBar(
            Updater updater, 
            CameraView cameraView, 
            Vector2 referenceResolution, 
            HealthStatusBarView statusBarView, 
            Collider2D unitCollider, 
            EntitySurvival entitySurvival)
        {
            _updater = updater;
            _mainCamera = cameraView.GetComponent<Camera>();
            _referenceResolution = referenceResolution;
            _statusBarView = statusBarView;
            _unitCollider = unitCollider;
            _entitySurvival = entitySurvival;


            if (_statusBarView is HealthShieldStatusBarView shieldStatusBarView)
            {
                _shieldStatusBarView = shieldStatusBarView;
            }
            
            InitStatusBarView();
            _updater.SubscribeToUpdate(FollowEnemy);
        }

        public void Dispose()
        {
            _updater.UnsubscribeFromUpdate(FollowEnemy);

            if (_entitySurvival == null) return;
            
            _entitySurvival.EntityHealth.HealthChanged -= UpdateHealthBar;

            if (_shieldStatusBarView != null)
            {
                _entitySurvival.EntityShield.ShieldChanged -= UpdateShieldBar;
                UnityEngine.Object.Destroy(_shieldStatusBarView.gameObject);
            }
            else if( _statusBarView != null)
            {
                UnityEngine.Object.Destroy(_statusBarView.gameObject);
            }
        }

        private void InitStatusBarView()
        {
            _statusBarView.HealthBar.Init(0f, _entitySurvival.EntityHealth.MaximumHealth,
                _entitySurvival.EntityHealth.CurrentHealth);

            _entitySurvival.EntityHealth.HealthChanged += UpdateHealthBar;

            if (_shieldStatusBarView != null)
            {
                _shieldStatusBarView.ShieldBar.Init(0f, _entitySurvival.EntityShield.MaximumShield,
                _entitySurvival.EntityShield.CurrentShield);

                _entitySurvival.EntityShield.ShieldChanged += UpdateShieldBar;
            }
        }

        private void UpdateHealthBar()
        {
            _statusBarView.HealthBar.UpdateValue(_entitySurvival.EntityHealth.CurrentHealth);
        }

        private void UpdateShieldBar()
        {
            _shieldStatusBarView.ShieldBar.UpdateValue(_entitySurvival.EntityShield.CurrentShield);
        }

        private void FollowEnemy()
        {
            if (_unitCollider == null || _entitySurvival == null)
            {
                Dispose();
                return;
            }

            var bounds = _unitCollider.bounds;
            bounds.Expand(HealthBarOffset * 2);

            if (UnityHelper.IsObjectVisible(_mainCamera, bounds))
            {
                _statusBarView.Show();
            }
            else
            {
                _statusBarView.Hide();
            }

            SetStatusBarPosition(_unitCollider.transform.position, (RectTransform)_statusBarView.transform);
        }

        private void SetStatusBarPosition(Vector3 unitPosition, RectTransform statusBarRectTransform)
        {
            var position = _mainCamera.WorldToScreenPoint(unitPosition + Vector3.up * HealthBarOffset);
            position = new Vector3(position.x - Screen.width / 2, position.y - Screen.height / 2, 0);
            var xScaleFactor = _referenceResolution.x / Screen.width;
            var yScaleFactor = _referenceResolution.y / Screen.height;
            var finalPosition = new Vector2(position.x * xScaleFactor, position.y * yScaleFactor);

            statusBarRectTransform.anchoredPosition = finalPosition;
        }
    }
}