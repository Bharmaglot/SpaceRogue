using System;
using SpaceRogue.Abstraction;
using UnityEngine;


namespace Gameplay.Shooting.Scriptables
{
    public abstract class WeaponConfig : ScriptableObject, IIdentityItem<string>
    {
        [field: SerializeField] public string Id { get; private set; } = Guid.NewGuid().ToString();
        [field: SerializeField, Min(0.1f)] public float Cooldown { get; private set; }
        [field: HideInInspector] public WeaponType Type { get; protected set; } = WeaponType.None;
    }
}