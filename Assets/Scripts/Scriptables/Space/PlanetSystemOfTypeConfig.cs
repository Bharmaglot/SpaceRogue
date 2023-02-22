using Gameplay.Space.Star;
using UnityEngine;
using Abstracts;

namespace Scriptables.Space
{
    [CreateAssetMenu(fileName = nameof(PlanetSystemOfTypeConfig), menuName = "Configs/Space/" + nameof(PlanetSystemOfTypeConfig))]
    public class PlanetSystemOfTypeConfig : ScriptableObject
    {
        [field: SerializeField] public PlanetarySystemConfig SunStarConfig { get; private set; }
        [field: SerializeField] public PlanetarySystemConfig WhiteDwarfConfig { get; private set; }
        [field: SerializeField] public PlanetarySystemConfig RedGiantConfig { get; private set; }
    }
}
