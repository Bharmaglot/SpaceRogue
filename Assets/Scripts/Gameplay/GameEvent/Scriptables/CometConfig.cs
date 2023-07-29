using SpaceRogue.Gameplay.GameEvent.Comet;
using UnityEngine;


namespace SpaceRogue.Gameplay.GameEvent.Scriptables
{
    [CreateAssetMenu(fileName = nameof(CometConfig), menuName = "Configs/Comet/" + nameof(CometConfig))]
    public sealed class CometConfig : ScriptableObject
    {
        [field: SerializeField] public CometView CometView { get; private set; }
        [field: SerializeField, Min(1)] public int CometCount { get; private set; } = 1;
        [field: SerializeField, Min(0.0f)] public float CheckRadius { get; private set; } = 5.0f;
        [field: SerializeField, Min(0.1f)] public float Size { get; private set; } = 1.0f;
        [field: SerializeField, Min(0.1f)] public float Damage { get; private set; } = 1.0f;
        [field: SerializeField, Tooltip("Seconds"), Min(0.1f)] public float LifeTime { get; private set; } = 10.0f;
        [field: SerializeField, Min(0.0f)] public float Offset { get; private set; } = 10.0f;
        [field: SerializeField, Min(0.0f)] public float MinSpeed { get; private set; } = 10.0f;
        [field: SerializeField, Min(0.0f)] public float MaxSpeed { get; private set; } = 50.0f;
    }
}