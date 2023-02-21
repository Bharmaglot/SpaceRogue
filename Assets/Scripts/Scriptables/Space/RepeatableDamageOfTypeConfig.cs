using Gameplay.Space.Star;
using UnityEngine;
using Abstracts;

namespace Scriptables.Space
{
    [CreateAssetMenu(fileName = nameof(RepeatableDamageOfTypeConfig), menuName = "Configs/Space/" + nameof(RepeatableDamageOfTypeConfig))]
    public class RepeatableDamageOfTypeConfig : ScriptableObject
    {
        [field: SerializeField] public RepeatableDamageConfig BlackHoleConfig { get; set; }
        [field: SerializeField] public RepeatableDamageConfig MagnetarConfig { get; set; }
    }
}