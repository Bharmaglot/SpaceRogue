using UnityEngine;


namespace SpaceRogue.Gameplay.Shooting.Scriptables
{
    [CreateAssetMenu(fileName = nameof(TorpedoConfig), menuName = "Configs/Projectiles/" + nameof(TorpedoConfig))]
    public sealed class TorpedoConfig : BaseReactiveObjectConfig
    {
        [field: SerializeField, Min(0.1f)] public float TimeToActiveGuard { get; private set; }
        [field: SerializeField, Min(0.1f)] public float ReactivateSpeed { get; private set; }
        [field: SerializeField, Min(0.1f)] public float RadarSignalSpeed { get; private set; }
        [field: SerializeField, Min(0.1f)] public float GuardTime { get; private set; }
    }
}
