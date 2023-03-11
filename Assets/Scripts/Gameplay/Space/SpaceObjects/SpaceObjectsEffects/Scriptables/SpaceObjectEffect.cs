using UnityEngine;

namespace SpaceObjects
{
    [CreateAssetMenu(fileName = nameof(SpaceObjectEffect), menuName = "Configs/Space/SpaceObjectEffects/" + nameof(SpaceObjectEffect))]
    public class SpaceObjectEffect : ScriptableObject
    {
        public SpaceObjectEffectType Type { get; private set; }
        public SpaceObjectEffectConfig Config { get; private set; }
    }
}
