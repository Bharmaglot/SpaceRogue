using UnityEngine;

namespace Gameplay.Missions.Scriptables
{
    [CreateAssetMenu(fileName = nameof(KillEnemiesMissionConfig), menuName = "Configs/Mission/" + nameof(KillEnemiesMissionConfig))]
    public class KillEnemiesMissionConfig : BaseMissionConfig
    {
        [field: SerializeField, Min(1)] public int KillCount { get; private set; } = 1;

        public KillEnemiesMissionConfig() => Type = MissionType.KillEnemies;
    }
}