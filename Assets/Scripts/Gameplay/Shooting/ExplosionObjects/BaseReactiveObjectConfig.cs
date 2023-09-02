using SpaceRogue.Enums;
using UnityEngine;


namespace SpaceRogue.Gameplay.Shooting.Scriptables
{
    public class BaseReactiveObjectConfig : ScriptableObject
    {
        [field: SerializeField] public ReactiveObjectView RocketPrefab { get; private set; }
        [field: SerializeField] public BaseExplosionObjectConfig ExplosionConfig { get; private set; }

        [field: SerializeField, Min(0.1f)] public float Speed { get; private set; }
        [field: SerializeField, Min(0.1f)] public float LifeTime { get; private set; }

        [field: SerializeField, Min(0.1f)] public float DistanceToTarget { get; private set; }
        [field: SerializeField, Min(0.1f)] public float RocketSize { get; private set; }

        [field: SerializeField] public EntityType TargetUnitType { get; private set; }
    }
}