using UnityEngine;
using Zenject;
using Gameplay.Shooting.Scriptables;

namespace Gameplay.Shooting.Factories
{
    public sealed class TurretViewFactory : PlaceholderFactory<Transform, TurretConfig, TurretView>
    {
        private readonly TurretView _prefab;
        private readonly DiContainer _diContainer;

        public TurretViewFactory(TurretView prefab, DiContainer diContainer)
        {
            _prefab = prefab;
            _diContainer = diContainer;
        }
        
        public override TurretView Create(Transform parentTransform, TurretConfig config)
        {
            var view = _diContainer.InstantiatePrefabForComponent<TurretView>(_prefab, parentTransform);
            var size = config.Range;
            var collider = view.GetComponent<CircleCollider2D>();
            collider.radius = size;
            return view;
        }
    }
}