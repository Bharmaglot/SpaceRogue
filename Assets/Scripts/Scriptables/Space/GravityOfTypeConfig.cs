using Gameplay.Space.Star;
using UnityEngine;
using Abstracts;

namespace Scriptables.Space
{
    [CreateAssetMenu(fileName = nameof(GravityOfTypeConfig), menuName = "Configs/Space/" + nameof(GravityOfTypeConfig))]
    public class GravityOfTypeConfig : ScriptableObject
    {
        [field: SerializeField] public GravityConfig BlackHoleConfig { get; set; }
        [field: SerializeField] public GravityConfig MagnetarConfig { get; set; }
    }
}