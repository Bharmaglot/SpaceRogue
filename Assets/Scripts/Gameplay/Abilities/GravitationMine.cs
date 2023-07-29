using System;
using Gameplay.Space.SpaceObjects.SpaceObjectsEffects;
using SpaceRogue.Gameplay.Abilities.Scriptables;
using Gameplay.Mechanics.Timer;


namespace SpaceRogue.Gameplay.Abilities
{
    public class GravitationMine : IDisposable
    {

        #region Fields

        private readonly AbilityView _view;
        private readonly GravitationAuraEffect _gravitationAuraEffects;
        private readonly Timer _timerToDestroy;

        #endregion


        #region CodeLife

        public GravitationMine(AbilityView view, GravitationAuraEffect gravitationAuraEffect, ShotgunAbilityConfig shotgunAbilityConfig, TimerFactory timerFactory)
        {
            _view = view;
            _gravitationAuraEffects = gravitationAuraEffect;
            _timerToDestroy = timerFactory.Create(shotgunAbilityConfig.LifeTime);
            _timerToDestroy.OnExpire += Dispose;
                
            _timerToDestroy.Start();

        }

        public void Dispose()
        {
            _timerToDestroy.OnExpire -= Dispose;
            _timerToDestroy.Dispose();
            _gravitationAuraEffects.Dispose();
            UnityEngine.Object.Destroy(_view.gameObject);
        }

        #endregion

    }
}