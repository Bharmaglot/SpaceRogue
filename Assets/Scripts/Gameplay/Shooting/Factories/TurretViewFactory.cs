using SpaceRogue.Gameplay.Shooting.Scriptables;
using UnityEngine;
using Zenject;


namespace SpaceRogue.Gameplay.Shooting.Factories
{
    public sealed class TurretViewFactory : PlaceholderFactory<Transform, TurretConfig, TurretView>
    {

        #region Fields

        private readonly TurretView _prefab;
        private readonly DiContainer _diContainer;

        #endregion


        #region CodeLife

        public TurretViewFactory(TurretView prefab, DiContainer diContainer)
        {
            _prefab = prefab;
            _diContainer = diContainer;
        }

        #endregion


        #region Methods

        public override TurretView Create(Transform parentTransform, TurretConfig config)
        {
            var view = _diContainer.InstantiatePrefabForComponent<TurretView>(_prefab, parentTransform);
            var size = config.Range;
            var collider = view.GetComponent<CircleCollider2D>();
            collider.radius = size;
            return view;
        }

        #endregion

    }
}