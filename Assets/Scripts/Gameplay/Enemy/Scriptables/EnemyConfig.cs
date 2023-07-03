using System;
using Gameplay.Enemy.Behaviour;
using Gameplay.Movement;
using Gameplay.Shooting.Scriptables;
using Gameplay.Survival;
using SpaceRogue.Abstraction;
using UnityEngine;


namespace Gameplay.Enemy.Scriptables
{
    [CreateAssetMenu(fileName = nameof(EnemyConfig), menuName = "Configs/Enemy/" + nameof(EnemyConfig))]
    public sealed class EnemyConfig : ScriptableObject, IIdentityItem<string>
    {
        [field: SerializeField] public string Id { get; private set; } = Guid.NewGuid().ToString();
        [field: SerializeField] public EnemyView Prefab { get; private set; }
        [field: SerializeField] public UnitMovementConfig Movement { get; private set; }
        [field: SerializeField] public EntitySurvivalConfig Survival { get; private set; }
        [field: SerializeField] public EnemyBehaviourConfig Behaviour { get; private set; }
        [field: SerializeField] public MountedWeaponConfig MountedWeapon { get; private set; }
    }
}