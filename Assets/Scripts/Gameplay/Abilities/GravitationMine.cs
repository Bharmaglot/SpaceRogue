using Gameplay.Mechanics.Timer;
using Gameplay.Space.SpaceObjects.SpaceObjectsEffects;
using SpaceRogue.Abstraction;
using SpaceRogue.Gameplay.Abilities.Scriptables;
using System;


namespace SpaceRogue.Gameplay.Abilities
{
    public class GravitationMine : IDisposable
    {

        #region Fields

        private readonly AbilityView _view;
        private readonly GravitationAuraEffect _gravitationAuraEffects;
        private readonly IDestroyable _destroyable;
        private readonly Timer _timerToDestroy;

        #endregion


        #region CodeLife

        public GravitationMine(
            AbilityView view,
            GravitationAuraEffect gravitationAuraEffect,
            ShotgunAbilityConfig shotgunAbilityConfig,
            TimerFactory timerFactory,
            IDestroyable destroyable)
        {
            _view = view;
            _gravitationAuraEffects = gravitationAuraEffect;
            _timerToDestroy = timerFactory.Create(shotgunAbilityConfig.LifeTime);
            _destroyable = destroyable;

            _destroyable.Destroyed += Dispose;
            _timerToDestroy.OnExpire += Dispose;    
            _timerToDestroy.Start();

        }

        public void Dispose()
        {
            _timerToDestroy.OnExpire -= Dispose;
            _timerToDestroy.Dispose();
            _gravitationAuraEffects.Dispose();

            if (_view != null)
            {
                UnityEngine.Object.Destroy(_view.gameObject); 
            }
        }

        #endregion

    }
}