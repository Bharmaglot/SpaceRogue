using Gameplay.Space.Star;
using UnityEngine;
using Abstracts;

namespace Scriptables.Space
{
    [CreateAssetMenu(fileName = nameof(GravityConfig), menuName = "Configs/Space/SpaceObjectEffects/" + nameof(GravityConfig))]
    public class GravityConfig : ScriptableObject
    {
        [field: SerializeField] public GravityView Prefab { get; private set; }
        [field: SerializeField] public float ForceGravity { get; private set; }
        [field: SerializeField, Range(1f, 5f)] public float RadiusGravity { get; private set; }
    }
}