using SpaceRogue.Enums;
using UnityEngine;


namespace SpaceRogue.Scriptables.GameEvent
{
    public abstract class GameEventConfig : ScriptableObject
    {
        [field: Header("Base Settings")]
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField, Min(0.0f)] public float IndicatorDiameter { get; private set; } = 500.0f;
        [field: SerializeField] public bool IsRecurring { get; private set; }
        [field: SerializeField] public bool ShowUntilItIsVisibleOnce { get; private set; } = false;
        [field: SerializeField, Tooltip("Seconds"), Min(0.1f)] public float ResponseTime { get; private set; } = 0.1f;
        [field: SerializeField, Range(0.01f, 1.0f)] public float Chance { get; private set; } = 1.0f;

        [field: HideInInspector] public GameEventType GameEventType { get; protected set; } = GameEventType.Empty;
    }
}