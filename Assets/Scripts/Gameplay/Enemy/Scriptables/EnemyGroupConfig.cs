using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Enemy.Scriptables
{
    [CreateAssetMenu(fileName = nameof(EnemyGroupConfig), menuName = "Configs/Enemy/" + nameof(EnemyGroupConfig))]
    public sealed class EnemyGroupConfig : ScriptableObject
    {
        [field: SerializeField] public List<EnemySquadConfig> Squads { get; private set; }
        [field: SerializeField] public float TimeToPickNewDirectionInSeconds { get; private set; } = 3;
    }
}