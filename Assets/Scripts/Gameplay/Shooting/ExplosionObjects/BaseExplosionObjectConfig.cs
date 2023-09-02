using SpaceRogue.Enums;
using UnityEngine;


namespace SpaceRogue.Gameplay.Shooting.Scriptables
{
    public abstract class BaseExplosionObjectConfig : ScriptableObject
    {
        [field: SerializeField, Min(0.1f)] public float DamageRadiusSize { get; private set; }
        [field: SerializeField, Min(0.1f)] public float DamageFromExplosion { get; private set; }
        [field: HideInInspector] public ExplosionEffectType Type { get; protected set; } = ExplosionEffectType.None;
    }
}
