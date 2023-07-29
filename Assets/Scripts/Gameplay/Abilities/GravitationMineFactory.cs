using Gameplay.Mechanics.Timer;
using Gameplay.Space.Factories;
using SpaceRogue.Gameplay.Abilities.Scriptables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


namespace SpaceRogue.Gameplay.Abilities
{
    public class GravitationMineFactory : PlaceholderFactory<AbilityView, Transform, ShotgunAbilityConfig, GravitationMine>
    {

        #region Fields

        private readonly AbilityViewFactory _gravitationMineViewFactory;
        private readonly GravitationAuraFactory _gravitationAuraFactory;
        private readonly TimerFactory _timerFactory;

        #endregion


        #region CodeLife

        public GravitationMineFactory(AbilityViewFactory gravitationMineViewFactory, GravitationAuraFactory gravitationAuraFactory, TimerFactory timerFactory)
        {
            _gravitationMineViewFactory = gravitationMineViewFactory;
            _gravitationAuraFactory = gravitationAuraFactory;
            _timerFactory = timerFactory;
        }

        #endregion


        #region Methods

        public override GravitationMine Create(AbilityView view, Transform transform, ShotgunAbilityConfig shotgunAbilityConfig)
        {
            var gravitationArea = _gravitationAuraFactory.Create(transform, shotgunAbilityConfig.GravitaionAreaConfig);
            return new GravitationMine(view, gravitationArea, shotgunAbilityConfig, _timerFactory);
        }

        #endregion

    }
}