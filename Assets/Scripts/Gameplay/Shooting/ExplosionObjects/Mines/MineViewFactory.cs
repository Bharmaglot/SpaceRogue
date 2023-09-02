using Gameplay.Pooling;
using SpaceRogue.Gameplay.Shooting.Scriptables;
using UnityEngine;
using Zenject;


namespace SpaceRogue.Gameplay.Shooting.Factories
{
    public sealed class MineViewFactory : PlaceholderFactory<Vector2, MineConfig, MineView>
    {

        #region Fields

        private readonly DiContainer _diContainer;
        private readonly Transform _projectilePoolTransform;

        #endregion


        #region CodeLife

        public MineViewFactory(DiContainer diContainer, ProjectilePool projectilePool)
        {
            _projectilePoolTransform = projectilePool.transform;
            _diContainer = diContainer;
        }

        #endregion


        #region Methods

        public override MineView Create(Vector2 position, MineConfig config)
        {
            var view = _diContainer.InstantiatePrefabForComponent<MineView>(config.MinePrefab, position, Quaternion.identity, _projectilePoolTransform);

            view.MineBodyTransform.localScale = new Vector2(config.MineSize, config.MineSize);

            view.AlertZoneTransform.localScale = new Vector2(config.MineSize + config.DamageRadiusSize, config.MineSize + config.DamageRadiusSize);
            view.AlertZoneTransform.gameObject.SetActive(false);

            view.TimerVisualTransform.localScale = view.MineBodyTransform.localScale;
            view.TimerVisualTransform.gameObject.SetActive(false);

            view.ExplosionTransform.localScale = view.MineBodyTransform.localScale;
            view.ExplosionTransform.gameObject.SetActive(false);

            return view;
        }

        #endregion

    }
}