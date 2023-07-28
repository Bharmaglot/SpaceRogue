using System;
using SpaceRogue.Abstraction;
using UnityEngine;


namespace SpaceRogue.Gameplay.Missions.Scriptables
{
    public abstract class BaseMissionConfig : ScriptableObject, IIdentityItem<string>
    {
        [field: SerializeField] public string Id { get; private set; } = Guid.NewGuid().ToString();
        [field: HideInInspector] public MissionType Type { get; protected set; } = MissionType.None;
    }
}