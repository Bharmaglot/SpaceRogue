using SpaceRogue.Enums;
using UnityEngine;


namespace SpaceRogue.Gameplay.Abilities.Scriptables
{
    [CreateAssetMenu(fileName = nameof(RailgunAbilityConfig), menuName = "Configs/Abilities/" + nameof(RailgunAbilityConfig))]
    public sealed class RailgunAbilityConfig : AbilityConfig
    {
        [field: SerializeField] public Gradient ShockwaveGradient { get; private set; }
        [field: SerializeField, Tooltip("Seconds"), Min(0)] public float ShockwaveLifetime { get; private set; } = 1;
        [field: SerializeField, Min(5)] public float ShockwaveRadius { get; private set; } = 10;
        [field: SerializeField, Min(0)] public float ShockwaveForce { get; private set; } = 50;

        public RailgunAbilityConfig()
        {
            Type = AbilityType.RailgunAbility;
        }
    }
}