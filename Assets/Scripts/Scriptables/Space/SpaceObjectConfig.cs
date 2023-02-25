using Gameplay.Space.Star;
using Gameplay.Space;
using UnityEngine;
using System.Collections.Generic;


namespace Scriptables.Space
{
    [CreateAssetMenu(fileName = nameof(SpaceObjectConfig), menuName = "Configs/Space/" + nameof(SpaceObjectConfig))]
    public sealed class SpaceObjectConfig : ScriptableObject
    {
        [field: SerializeField, Header("Prefab")] public SpaceObjectView Prefab { get; private set; }

        [field: SerializeField, Min(5f), Header("Size")] public float MinSize { get; private set; } = 5f;
        [field: SerializeField, Min(5.1f)] public float MaxSize { get; private set; } = 5.1f;

        [field: SerializeField] public List<SpaceObjectEffectConfig> Effects { get; private set; }
    }
}