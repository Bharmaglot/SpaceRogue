using SpaceRogue.Gameplay.Abilities.Scriptables;
using SpaceRogue.Gameplay.Pooling;
using UnityEngine;
using Zenject;


namespace SpaceRogue.Gameplay.Abilities
{
    public sealed class AbilityViewFactory : PlaceholderFactory<Vector2, AbilityConfig, AbilityView>
    {

        #region Fields

        private readonly AbilityPool _abilityPool;
        private readonly DiContainer _diContainer;

        #endregion


        #region CodeLife

        public AbilityViewFactory(AbilityPool abilityPool, DiContainer diContainer)
        {
            _abilityPool = abilityPool;
            _diContainer = diContainer;
        }

        #endregion


        #region Methods

        public override AbilityView Create(Vector2 position, AbilityConfig abilityConfig)
            => _diContainer.InstantiatePrefabForComponent<AbilityView>(abilityConfig.AbilityPrefab, position, Quaternion.identity, _abilityPool.transform);

        #endregion

    }
}