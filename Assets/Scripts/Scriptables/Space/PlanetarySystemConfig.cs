using Gameplay.Space.Star;
using UnityEngine;
using Abstracts;

namespace Scriptables.Space
{
    [CreateAssetMenu(fileName = nameof(PlanetarySystemConfig), menuName = "Configs/Space/" + nameof(PlanetarySystemConfig))]
    public class PlanetarySystemConfig : ScriptableObject
    {
        [field: SerializeField] public int MinPlanetCount { get; private set; }
        [field: SerializeField] public int MaxPlanetCount { get; private set; }

        [field: SerializeField] public int MinOrbit { get; private set; }
        [field: SerializeField] public int MaxOrbit { get; private set; }
    }
}
