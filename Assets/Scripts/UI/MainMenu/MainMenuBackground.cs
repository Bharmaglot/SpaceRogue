using Scriptables;
using SpaceRogue.Gameplay.Background;
using SpaceRogue.Services;
using System;
using UnityEngine;


namespace UI.MainMenu
{
    public sealed class MainMenuBackground : IDisposable
    {

        #region Fields

        private readonly Camera _camera;
        private readonly Updater _updater;
        private readonly MenuBackgroundConfig _config;
        private readonly Transform _target;

        private readonly InfiniteSprite _backParallax;
        private readonly InfiniteSprite _midParallax;
        private readonly InfiniteSprite _foreParallax;
        private readonly NebulaEffect _nebulaEffect;

        #endregion


        #region CodeLife

        public MainMenuBackground(Camera camera, Updater updater, MenuBackgroundView view, MenuBackgroundConfig config)
        {
            _camera = camera;
            _updater = updater;
            _config = config;
            _target = camera.transform;

            _backParallax = new(_target, view.BackSpriteRenderer, _config.BackCoefficient);
            _midParallax = new(_target, view.MidSpriteRenderer, _config.MidCoefficient);
            _foreParallax = new(_target, view.ForeSpriteRenderer, _config.ForeCoefficient);
            _nebulaEffect = new(_target, view.NebulaParticleSystem.transform, _config.NebulaCoefficient);

            _updater.SubscribeToLateUpdate(PlayAllEffects);
        }

        public void Dispose() => _updater.UnsubscribeFromLateUpdate(PlayAllEffects);

        #endregion


        #region Methods

        private void PlayAllEffects()
        {
            var mousePoint = _camera.ScreenToViewportPoint(Input.mousePosition) * _config.CameraCoefficient;
            _target.position = new(mousePoint.x, mousePoint.y, _target.position.z);

            _backParallax.Play();
            _midParallax.Play();
            _foreParallax.Play();

            _nebulaEffect.Play();
        }

        #endregion

    }
}