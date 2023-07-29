using Gameplay.GameEvent.Caravan;
using Gameplay.Movement;
using Gameplay.Survival.Health;
using Gameplay.Survival.Shield;
using UnityEngine;


namespace SpaceRogue.Scriptables.GameEvent
{
    [CreateAssetMenu(fileName = nameof(CaravanConfig), menuName = "Configs/Caravan/" + nameof(CaravanConfig))]
    public sealed class CaravanConfig : ScriptableObject
    {
        [field: SerializeField] public CaravanView CaravanView { get; private set; }
        [field: SerializeField] public UnitMovementConfig UnitMovement { get; private set; }
        [field: SerializeField] public HealthConfig Health { get; private set; }
        [field: SerializeField] public ShieldConfig Shield { get; private set; }
    }
}