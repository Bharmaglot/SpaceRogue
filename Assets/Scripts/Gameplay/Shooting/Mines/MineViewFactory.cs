using Gameplay.Pooling;
using Gameplay.Shooting.Scriptables;
using SpaceRogue.Shooting;
using UnityEngine;
using Zenject;


namespace Gameplay.Shooting.Factories
{
    public class MineViewFactory : PlaceholderFactory<Vector2, MineConfig, MineView>
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
            MineView view = _diContainer.InstantiatePrefabForComponent<MineView>(config.Prefab, position, Quaternion.identity, _projectilePoolTransform);
            
            view.MineBodyTransform.localScale = new Vector2(config.MineSize, config.MineSize);
            
            view.MineAlertZoneTansform.localScale = new Vector2(config.MineSize + config.AlarmZoneRadius, config.MineSize + config.AlarmZoneRadius);
            view.MineAlertZoneTansform.gameObject.SetActive(false);
            
            view.MineTimerVisualTransform.localScale = view.MineBodyTransform.localScale;
            view.MineTimerVisualTransform.gameObject.SetActive(false);
            
            view.ExplozionTransform.localScale = view.MineBodyTransform.localScale;
            view.ExplozionTransform.gameObject.SetActive(false);

            return view;
        }

        #endregion

    }
}