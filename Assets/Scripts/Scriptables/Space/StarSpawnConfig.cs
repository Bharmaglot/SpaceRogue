using System.Collections.Generic;
using UnityEngine;

namespace Scriptables.Space
{
    [CreateAssetMenu(fileName = nameof(StarSpawnConfig), menuName = "Configs/Space/" + nameof(StarSpawnConfig))]
    public sealed class StarSpawnConfig : ScriptableObject
    {
        [field: SerializeField] public List<WeightConfig<SpaceObjectConfig>> WeightConfigs { get; private set; }
    }
}