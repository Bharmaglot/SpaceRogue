using SpaceRogue.Enums;
using UnityEngine;


namespace SpaceRogue.Gameplay.Abilities.Scriptables
{
    [CreateAssetMenu(fileName = nameof(MinigunAbilityConfig), menuName = "Configs/Abilities/" + nameof(MinigunAbilityConfig))]
    public sealed class MinigunAbilityConfig : AbilityConfig
    {
        [field: SerializeField] public float AccelerationBoost { get; private set; } = 10.0f;
        [field: SerializeField, Tooltip("Seconds"), Min(0.1f)] public float AccelerationDuration { get; private set; }
        [field: SerializeField, Tooltip("Seconds"), Min(0.1f)] public float DamageAndCollisionIgnoreDuration { get; private set; }

        public MinigunAbilityConfig()
        {
            Type = AbilityType.MinigunAbility;
        }
    }
}