using UnityEngine;
using Zenject;


namespace SpaceRogue.Gameplay.Shooting.Factories
{
    public sealed class GunPointViewFactory : PlaceholderFactory<Vector2, Quaternion, Transform, GunPointView>
    {

        #region Fields

        private readonly GunPointView _prefab;
        private readonly DiContainer _diContainer;

        #endregion


        #region CodeLife

        public GunPointViewFactory(GunPointView prefab, DiContainer diContainer)
        {
            _prefab = prefab;
            _diContainer = diContainer;
        }

        #endregion


        #region Methods

        public override GunPointView Create(Vector2 position, Quaternion rotation, Transform parentTransform)
            => _diContainer.InstantiatePrefabForComponent<GunPointView>(_prefab, position, rotation, parentTransform);

        #endregion

    }
}