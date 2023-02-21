using Gameplay.Space.Star;
using UnityEngine;
using Abstracts;

namespace Scriptables.Space
{
    [CreateAssetMenu(fileName = nameof(RepeatableDamageConfig), menuName = "Configs/Space/SpaceObjectEffects/" + nameof(RepeatableDamageConfig))]
    public class RepeatableDamageConfig : ScriptableObject
    {
        [field: SerializeField] public DamageZoneView Prefab { get; private set; }
        [field: SerializeField, Range(1f, 5f)] public float DamageSize { get; private set; }
        [field: SerializeField] public int DamageValue { get; private set; }
        [field: SerializeField, Min(1.1f)] public float DamageCooldown { get; private set; }
    }
}