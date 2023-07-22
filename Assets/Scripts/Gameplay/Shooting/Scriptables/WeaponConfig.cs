using SpaceRogue.Abstraction;
using SpaceRogue.Enums;
using System;
using UnityEngine;


namespace SpaceRogue.Gameplay.Shooting.Scriptables
{
    public abstract class WeaponConfig : ScriptableObject, IIdentityItem<string>
    {
        [field: SerializeField] public string Id { get; private set; } = Guid.NewGuid().ToString();
        [field: SerializeField, Tooltip("Seconds"), Min(0.1f)] public float Cooldown { get; private set; }
        [field: HideInInspector] public WeaponType Type { get; protected set; } = WeaponType.None;
    }
}