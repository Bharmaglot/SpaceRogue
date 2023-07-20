using SpaceRogue.Enums;
using UnityEngine;


namespace SpaceRogue.Gameplay.Abilities.Scriptables
{
    public abstract class AbilityConfig : ScriptableObject
    {
        [field: SerializeField] public GameObject AbilityPrefab { get; private set; }
        [field: SerializeField, Min(0.1f)] public float Cooldown { get; private set; }
        [field: HideInInspector] public AbilityType Type { get; protected set; } = AbilityType.None;
    }
}