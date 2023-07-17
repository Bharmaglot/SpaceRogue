using Gameplay.Camera;
using Scriptables;
using SpaceRogue.Services;
using System;
using UnityEngine;


namespace Gameplay.Background
{
    public sealed class GameBackground : IDisposable
    {
        private const int MaskCoefficient = 1;

        private readonly Updater _updater;
        private readonly GameBackgroundConfig _config;
        private readonly Transform _target;

        private readonly InfiniteSprite _backParalax;
        private readonly InfiniteSprite _midParalax;
        private readonly InfiniteSprite _foreParalax;

        private readonly NebulaEffect _nebulaBackEffect;
        private readonly NebulaEffect _nebulaForeEffect;
        private readonly NebulaEffect _nebulaMaskEffect;

        public GameBackground(Updater updater, CameraView camera, GameBackgroundView view, GameBackgroundConfig config)
        {
            _updater = updater;
            _config = config;
            _target = camera.transform;

            _backParalax = new(_target, view.BackSpriteRenderer, _config.BackCoefficient);
            _midParalax = new(_target, view.MidSpriteRenderer, _config.MidCoefficient);
            _foreParalax = new(_target, view.ForeSpriteRenderer, _config.ForeCoefficient);

            _nebulaBackEffect = new(_target, view.NebulaBackParticleSystem.transform, _config.NebulaBackCoefficient);
            _nebulaForeEffect = new(_target, view.NebulaForeParticleSystem.transform, _config.NebulaForeCoefficient);
            _nebulaMaskEffect = new(_target, view.NebulaMaskParticleSystem.transform, MaskCoefficient);

            _updater.SubscribeToLateUpdate(PlayAllEffects);
        }

        public void Dispose()
        {
            _updater.UnsubscribeFromLateUpdate(PlayAllEffects);
        }

        private void PlayAllEffects()
        {
            _backParalax.Play();
            _midParalax.Play();
            _foreParalax.Play();

            _nebulaBackEffect.Play();
            _nebulaForeEffect.Play();
            _nebulaMaskEffect.Play();
        }
    }
}