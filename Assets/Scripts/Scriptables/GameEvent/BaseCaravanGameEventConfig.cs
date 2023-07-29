using Gameplay.Enemy.Scriptables;
using UnityEngine;


namespace SpaceRogue.Scriptables.GameEvent
{
    public class BaseCaravanGameEventConfig : GameEventConfig
    {
        [field: SerializeField, Header("Base Caravan Settings")] public CaravanConfig CaravanConfig{ get; private set; }
        [field: SerializeField, Min(0.0f)] public float SpawnOffset { get; private set; } = 5.0f;
        [field: SerializeField, Min(0.0f)] public float PathDistance { get; private set; } = 100.0f;
        [field: SerializeField, Header("Enemy Settings")] public LegacyEnemyConfig LegacyEnemyConfig { get; private set; }
        [field: SerializeField, Min(0)] public int EnemyCount { get; private set; } = 1;
    }
}