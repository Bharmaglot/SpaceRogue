using UnityEngine;
using System.Collections.Generic;

namespace Scriptables.Space
{
    [CreateAssetMenu(fileName = nameof(PlanetSystemConfig), menuName = "Configs/Space/SpaceObjectEffects/" + nameof(PlanetSystemConfig))]
    public class PlanetSystemConfig : SpaceObjectEffectConfig
    {
        [field: SerializeField, Min(0), Header("Planets")] public int MinPlanetCount { get; private set; } = 0;
        [field: SerializeField, Min(0)] public int MaxPlanetCount { get; private set; } = 4;

        [field: SerializeField, Min(0.1f), Header("Planet Orbits")] public float MinOrbit { get; private set; } = 0.1f;
        [field: SerializeField, Min(1f)] public float MaxOrbit { get; private set; } = 1f;

        [field: SerializeField] public List<WeightConfig<PlanetConfig>> WeightConfigs { get; private set; }
    }
}
