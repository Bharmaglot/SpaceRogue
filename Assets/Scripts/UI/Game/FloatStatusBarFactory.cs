using Zenject;
using Gameplay.Survival;
using Gameplay.Camera;
using SpaceRogue.Services;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Game
{
    public sealed class FloatStatusBarFactory : PlaceholderFactory<HealthStatusBarView, Collider2D, EntitySurvival, FloatStatusBar>
    {
        private readonly Updater _updater;
        private readonly CameraView _cameraView;
        private readonly Vector2 _referenceResolution;

        public FloatStatusBarFactory(Updater updater, CameraView cameraView, MainCanvas mainCanvas)
        {
            _updater = updater;
            _cameraView = cameraView;
            _referenceResolution = mainCanvas.GetComponentInParent<CanvasScaler>().referenceResolution;
        }

        public override FloatStatusBar Create(HealthStatusBarView statusBarView, Collider2D unitCollider, EntitySurvival entitySurvival)
        {
            return new FloatStatusBar(_updater, _cameraView, _referenceResolution, statusBarView, unitCollider, entitySurvival);
        }
    }
}