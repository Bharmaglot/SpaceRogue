using Gameplay.Space.Star;
using UnityEngine;
using Abstracts;

namespace Scriptables.Space
{
    [CreateAssetMenu(fileName = nameof(StarConfig), menuName = "Configs/Space/" + nameof(StarConfig))]
    public class StarConfig : ScriptableObject
    {
        [field: SerializeField, Header("Prefab")] public StarView Prefab { get; private set; }

        [field: SerializeField, Min(5f), Header("Size")] public float MinSize { get; private set; } = 5f;
        [field: SerializeField, Min(5.1f)] public float MaxSize { get; private set; } = 5.1f;

        [field: SerializeField] internal SpaceObjectType Type;
    }
}