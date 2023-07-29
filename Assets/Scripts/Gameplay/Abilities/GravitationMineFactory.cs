using Gameplay.Mechanics.Timer;
using Gameplay.Space.Factories;
using SpaceRogue.Abstraction;
using SpaceRogue.Gameplay.Abilities.Scriptables;
using UnityEngine;
using Zenject;


namespace SpaceRogue.Gameplay.Abilities
{
    public class GravitationMineFactory : PlaceholderFactory<AbilityView, Transform, ShotgunAbilityConfig, IDestroyable, GravitationMine>
    {

        #region Fields

        private readonly GravitationAuraFactory _gravitationAuraFactory;
        private readonly TimerFactory _timerFactory;

        #endregion


        #region CodeLife

        public GravitationMineFactory(GravitationAuraFactory gravitationAuraFactory, TimerFactory timerFactory)
        {
            _gravitationAuraFactory = gravitationAuraFactory;
            _timerFactory = timerFactory;
        }

        #endregion


        #region Methods

        public override GravitationMine Create(AbilityView view, Transform transform, ShotgunAbilityConfig shotgunAbilityConfig, IDestroyable destroyable)
        {
            var gravitationArea = _gravitationAuraFactory.Create(transform, shotgunAbilityConfig.GravitaionAreaConfig);
            return new GravitationMine(view, gravitationArea, shotgunAbilityConfig, _timerFactory, destroyable);
        }

        #endregion

    }
}