using Gameplay.Pooling;
using SpaceRogue.Gameplay.Shooting.Scriptables;
using UnityEngine;
using Zenject;


namespace SpaceRogue.Gameplay.Shooting.Factories
{
    public class RocketViewFactory : PlaceholderFactory<Vector2, Quaternion, BaseReactiveObjectConfig, ReactiveObjectView>
    {

        #region Fields

        private readonly DiContainer _diContainer;
        private readonly Transform _projectilePoolTransform;

        #endregion


        #region CodeLife

        public RocketViewFactory(DiContainer diContainer, ProjectilePool projectilePool)
        {
            _projectilePoolTransform = projectilePool.transform;
            _diContainer = diContainer;
        }

        #endregion


        #region Methods

        public override ReactiveObjectView Create(Vector2 position, Quaternion quaternion, BaseReactiveObjectConfig config)
        {
            var view = _diContainer.InstantiatePrefabForComponent<ReactiveObjectView>(config.RocketPrefab, position, quaternion, _projectilePoolTransform);
            view.transform.localScale = new Vector2(config.RocketSize, config.RocketSize);
            view.TargetFinderView.transform.localScale = new Vector2 (config.DistanceToTarget, config.DistanceToTarget);

            return view;
        }

        #endregion

    }
}

