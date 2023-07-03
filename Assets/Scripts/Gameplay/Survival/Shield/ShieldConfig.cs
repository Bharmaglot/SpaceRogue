using SpaceRogue.Abstraction;
using UnityEngine;


namespace Gameplay.Survival.Shield
{
    [CreateAssetMenu(fileName = nameof(ShieldConfig), menuName = "Configs/Survival/" + nameof(ShieldConfig))]
    public sealed class ShieldConfig : ScriptableObject, IShieldInfo
    {
        [field: SerializeField, Min(1f)] public float MaximumShield { get; private set; } = 1.0f;
        [field: SerializeField, Min(1f)] public float StartingShield { get; private set; } = 1.0f;
        [field: SerializeField, Min(0.1f)] public float Cooldown { get; private set; } = 0.1f;
    }
}