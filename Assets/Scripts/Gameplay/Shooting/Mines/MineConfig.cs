using SpaceRogue.Enums;
using UnityEngine;

namespace Gameplay.Shooting.Scriptables
{
    [CreateAssetMenu(fileName = nameof(MineConfig), menuName = "Configs/Projectiles/" + nameof(MineConfig))]
    public class MineConfig : ScriptableObject
    {

        [field: SerializeField] public MineView Prefab { get; private set; }

        [field: SerializeField, Min(0.1f)] public float MineSize { get; private set; }
        [field: SerializeField, Min(0.1f)] public float AlarmZoneRadius { get; private set; }
        [field: SerializeField, Min(0f)] public float TimeToActiveAlarmSystem { get; private set; }
        [field: SerializeField, Min(0f)] public float TimeToExplosion { get; private set; }
        [field: SerializeField, Min(0.1f)] public float DamageFromExplosion { get; private set; }
        [field: SerializeField, Min(0.1f)] public float SpeedWaveExplosion { get; private set; }
        [field: SerializeField, Min(0.1f)] public EntityType TargetUnitType { get; private set; }
    }
}
