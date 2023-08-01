using Scriptables;
using SpaceRogue.Gameplay.Camera;
using SpaceRogue.Services;
using System;
using UnityEngine;


namespace SpaceRogue.Gameplay.Background
{
    public sealed class GameBackground : IDisposable
    {

        #region Fields

        private const int MASK_COEFFICIENT = 1;

        private readonly Updater _updater;
        private readonly GameBackgroundConfig _config;
        private readonly Transform _target;

        private readonly InfiniteSprite _backParallax;
        private readonly InfiniteSprite _midParallax;
        private readonly InfiniteSprite _foreParallax;

        private readonly NebulaEffect _nebulaBackEffect;
        private readonly NebulaEffect _nebulaForeEffect;
        private readonly NebulaEffect _nebulaMaskEffect;

        #endregion


        #region CodeLife

        public GameBackground(Updater updater, CameraView camera, GameBackgroundView view, GameBackgroundConfig config)
        {
            _updater = updater;
            _config = config;
            _target = camera.transform;

            _backParallax = new(_target, view.BackSpriteRenderer, _config.BackCoefficient);
            _midParallax = new(_target, view.MidSpriteRenderer, _config.MidCoefficient);
            _foreParallax = new(_target, view.ForeSpriteRenderer, _config.ForeCoefficient);

            _nebulaBackEffect = new(_target, view.NebulaBackParticleSystem.transform, _config.NebulaBackCoefficient);
            _nebulaForeEffect = new(_target, view.NebulaForeParticleSystem.transform, _config.NebulaForeCoefficient);
            _nebulaMaskEffect = new(_target, view.NebulaMaskParticleSystem.transform, MASK_COEFFICIENT);

            _updater.SubscribeToLateUpdate(PlayAllEffects);
        }

        public void Dispose()
        {
            _updater.UnsubscribeFromLateUpdate(PlayAllEffects);
        }

        #endregion


        #region Methods

        private void PlayAllEffects()
        {
            _backParallax.Play();
            _midParallax.Play();
            _foreParallax.Play();

            _nebulaBackEffect.Play();
            _nebulaForeEffect.Play();
            _nebulaMaskEffect.Play();
        }

        #endregion

    }
}