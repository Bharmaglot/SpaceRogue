using System;
using System.Collections.Generic;
using Gameplay.Enemy.Behaviour;
using Gameplay.Movement;
using Gameplay.Shooting.Scriptables;
using Gameplay.Survival.Health;
using Gameplay.Survival.Shield;
using Scriptables;
using SpaceRogue.Abstraction;
using UnityEngine;


namespace Gameplay.Enemy.Scriptables
{
    [CreateAssetMenu(fileName = nameof(LegacyEnemyConfig), menuName = "Configs/Enemy/" + nameof(LegacyEnemyConfig))]
    public sealed class LegacyEnemyConfig : ScriptableObject, IIdentityItem<string>
    {
        [field: SerializeField] public string Id { get; private set; } = Guid.NewGuid().ToString();
        [field: SerializeField] public EnemyView Prefab { get; private set; }
        [field: SerializeField] public List<WeightConfig<WeaponConfig>> TurretConfigs { get; private set; }
        [field: SerializeField] public UnitMovementConfig UnitMovement { get; private set; }
        [field: SerializeField] public EnemyBehaviourConfig Behaviour { get; private set; }
        [field: SerializeField] public HealthConfig Health { get; private set; }
        [field: SerializeField] public ShieldConfig Shield { get; private set; }
    }
}