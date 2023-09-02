using UnityEngine;


namespace SpaceRogue.Gameplay.Shooting.Scriptables
{
    [CreateAssetMenu(fileName = nameof(RocketConfig), menuName = "Configs/Projectiles/" + nameof(RocketConfig))]
    public sealed class RocketConfig : BaseReactiveObjectConfig
    {
        [field: SerializeField, Min(0.0f)] public float RotateSpeed { get; private set; }
    }
}
