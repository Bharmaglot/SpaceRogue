using Gameplay.Damage;
using Gameplay.Pooling;
using SpaceRogue.Enums;
using SpaceRogue.Gameplay.Shooting.Scriptables;
using System;
using UnityEngine;
using Zenject;


namespace SpaceRogue.Gameplay.Shooting.Factories
{
    public sealed class ExplosionViewFactory : PlaceholderFactory<Vector2, BaseExplosionObjectConfig, ExplosionView>
    {

        #region Fields

        private readonly DiContainer _diContainer;
        private readonly Transform _projectilePoolTransform;

        #endregion


        #region CodeLife

        public ExplosionViewFactory(DiContainer diContainer, ProjectilePool projectilePool)
        {
            _projectilePoolTransform = projectilePool.transform;
            _diContainer = diContainer;
        }

        #endregion


        #region Methods

        public override ExplosionView Create(Vector2 position, BaseExplosionObjectConfig config)
        {
            return config.Type switch
            {
                ExplosionEffectType.None => new ExplosionView(),
                ExplosionEffectType.InstantExplosion => CreateExplosion(config as InstantExplosionConfig, position),
                ExplosionEffectType.WaveExplosion => CreateExplosion(config as WaveExplosionConfig, position),
                _ => throw new ArgumentOutOfRangeException(nameof(config.Type), config.Type, "A not-existent game event type is provided")
            };
        }

        private ExplosionView CreateExplosion(InstantExplosionConfig config, Vector2 position)
        {
            var view = _diContainer.InstantiatePrefabForComponent<ExplosionView>(config.Prefab, position, Quaternion.identity, _projectilePoolTransform);
            view.ExplosionTransform.localScale = new Vector2(0.0f, 0.0f);
            view.DamageExplosionView.Init(new DamageModel(config.DamageFromExplosion));
            return view;
        }

        private ExplosionView CreateExplosion(WaveExplosionConfig config, Vector2 position)
        {
            var view = _diContainer.InstantiatePrefabForComponent<ExplosionView>(config.Prefab, position, Quaternion.identity, _projectilePoolTransform);
            view.ExplosionTransform.localScale = new Vector2(0.0f, 0.0f);
            view.DamageExplosionView.Init(new DamageModel(config.DamageFromExplosion));
            return view;
        }

        #endregion

    }
}