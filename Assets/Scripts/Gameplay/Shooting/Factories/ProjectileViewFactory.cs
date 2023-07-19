using Gameplay.Damage;
using Gameplay.Pooling;
using UnityEngine;
using Utilities.Mathematics;
using Zenject;


namespace SpaceRogue.Gameplay.Shooting.Factories
{
    public sealed class ProjectileViewFactory : PlaceholderFactory<ProjectileSpawnParams, ProjectileView>
    {
        #region Fields

        private const float ANGLE_CORRECTION = 90f;

        private readonly DiContainer _diContainer;
        private readonly Transform _projectilePoolTransform;

        #endregion

        #region CodeLife

        public ProjectileViewFactory(DiContainer diContainer, ProjectilePool projectilePool)
        {
            _diContainer = diContainer;
            _projectilePoolTransform = projectilePool.transform;
        }

        #endregion

        #region Methods

        public override ProjectileView Create(ProjectileSpawnParams spawnParams)
        {
            var (position, rotation, config, unitType) = spawnParams;
            var projectileView = _diContainer.InstantiatePrefabForComponent<ProjectileView>(config.Prefab, position, rotation, _projectilePoolTransform);
            projectileView.Init(new DamageModel(config.DamageAmount, unitType));
            var direction = CalculateProjectileDirection(position, rotation);
            projectileView.GetComponent<Rigidbody2D>().velocity = direction * config.Speed;
            return projectileView;
        }

        private Vector2 CalculateProjectileDirection(Vector2 position, Quaternion rotation)
        {
            var unitCircleDirection = (Vector2)MathExtensions.ToVector3(rotation.eulerAngles.z + ANGLE_CORRECTION);
            var globalScaleDestination = new Vector2(position.x + unitCircleDirection.x, position.y + unitCircleDirection.y);
            var direction = globalScaleDestination - position;
            return direction;
        }

        #endregion
    }
}