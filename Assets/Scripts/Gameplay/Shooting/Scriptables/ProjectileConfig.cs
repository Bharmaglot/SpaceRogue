using SpaceRogue.Abstraction;
using System;
using UnityEngine;


namespace SpaceRogue.Gameplay.Shooting.Scriptables
{
    [CreateAssetMenu(fileName = nameof(ProjectileConfig), menuName = "Configs/Projectiles/" + nameof(ProjectileConfig))]
    public sealed class ProjectileConfig : ScriptableObject, IIdentityItem<string>
    {
        [field: SerializeField] public string Id { get; private set; } = Guid.NewGuid().ToString();
        [field: SerializeField] public ProjectileView Prefab { get; private set; }
        [field: SerializeField, Min(0.1f)] public float DamageAmount { get; private set; } = 1f;
        [field: SerializeField, Min(0.01f)] public float Speed { get; private set; } = 1f;
        [field: SerializeField, Min(0.1f)] public float LifeTime { get; private set; } = 10.0f;
        [field: SerializeField] public bool IsDestroyedOnHit { get; private set; } = true;
    }
}