using UnityEngine;


namespace Gameplay.Movement
{
    [CreateAssetMenu(fileName = nameof(UnitMovementConfig), menuName = "Configs/Movement/" + nameof(UnitMovementConfig))]
    public sealed class UnitMovementConfig : ScriptableObject
    {
        [Header("Speed")] 
        [Min(0.1f)]
        [SerializeField] public float maximumSpeed = 0.1f;
        [Min(0.1f)]
        [SerializeField] public float maximumBackwardSpeed = 0.1f;
        [Min(0.1f)]
        [SerializeField] public float accelerationTime = 0.1f;
        [Min(0.1f)]
        [SerializeField] public float stoppingSpeed = 0.3f;

        [Header("Turn speed")] 
        [Min(0.1f)]
        [SerializeField] public float startingTurnSpeed = 0.1f;
        [Min(0.1f)]
        [SerializeField] public float maximumTurnSpeed = 0.1f;
        [Min(0.1f)]
        [SerializeField] public float turnAccelerationTime = 0.1f;

        [field: Header("Dash")]
        [field: SerializeField, Tooltip("Units")] public float DashLength { get; private set; } = 0.1f;
        [field: SerializeField, Tooltip("Seconds")] public float DashCooldown { get; private set; } = 0.1f;
    }
}