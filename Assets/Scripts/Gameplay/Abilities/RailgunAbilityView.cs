using UnityEngine;


namespace SpaceRogue.Gameplay.Abilities
{
    public sealed class RailgunAbilityView : AbilityView
    {
        #region Fields

        [SerializeField] private ParticleSystem _particleSystem;

        #endregion

        #region Methods

        public void SetShockwaveEffectSettings(Gradient gradient, float shockwaveLifetime, float shockwaveRadius)
        {
            var mainModule = _particleSystem.main;
            mainModule.startLifetime = shockwaveLifetime;
            mainModule.startSize = shockwaveRadius;
            var minMaxGradient = _particleSystem.colorOverLifetime.color;
            minMaxGradient.gradient = gradient;
        }

        #endregion
    }
}