using System;
using Gameplay.Space.SpaceObjects;
using Gameplay.Space.SpaceObjects.Scriptables;
using Gameplay.Space.SpaceObjects.SpaceObjectsEffects;
using UnityEngine;
using Zenject;

namespace Gameplay.Space.Factories
{
    public class SpaceObjectEffectFactory : IFactory<Vector3, SpaceObjectEffectConfig, SpaceObjectEffect>
    {
        public SpaceObjectEffectFactory()
        {
            //TODO init factories
        }

        public SpaceObjectEffect Create(Vector3 spaceObjectScale, SpaceObjectEffectConfig config)
        {
            return config.Type switch
            {
                SpaceObjectEffectType.None => new SpaceObjectEmptyEffect(),
                SpaceObjectEffectType.PlanetSystem => new PlanetSystemEffect(),
                SpaceObjectEffectType.DamageAura => new DamageAuraEffect(),
                SpaceObjectEffectType.GravitationAura => new GravitationAuraEffect(),
                SpaceObjectEffectType.DamageOnTouch => new DamageOnTouchEffect(),
                _ => throw new ArgumentOutOfRangeException(nameof(config.Type), config.Type, "A not-existent game event type is provided")
            };
        }
    }
}

