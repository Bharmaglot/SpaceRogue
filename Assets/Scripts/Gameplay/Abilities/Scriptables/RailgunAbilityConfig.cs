using SpaceRogue.Enums;
using UnityEngine;


namespace SpaceRogue.Gameplay.Abilities.Scriptables
{
    [CreateAssetMenu(fileName = nameof(RailgunAbilityConfig), menuName = "Configs/Abilities/" + nameof(RailgunAbilityConfig))]
    public sealed class RailgunAbilityConfig : AbilityConfig
    {
        [field: SerializeField] public Gradient ShockwaveGradient { get; private set; }
        [field: SerializeField, Tooltip("Seconds"), Min(0.0f)] public float ShockwaveLifetime { get; private set; } = 1.0f;
        [field: SerializeField, Min(5.0f)] public float ShockwaveRadius { get; private set; } = 10.0f;
        [field: SerializeField, Min(0.0f)] public float ShockwaveForce { get; private set; } = 50.0f;

        public RailgunAbilityConfig()
        {
            Type = AbilityType.RailgunAbility;
        }
    }
}