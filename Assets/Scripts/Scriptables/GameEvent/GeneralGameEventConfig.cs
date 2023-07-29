using System.Collections.Generic;
using UnityEngine;


namespace SpaceRogue.Scriptables.GameEvent
{
    [CreateAssetMenu(fileName = nameof(GeneralGameEventConfig), menuName = "Configs/GameEvent/" + nameof(GeneralGameEventConfig))]
    public sealed class GeneralGameEventConfig : ScriptableObject
    {
        [field: SerializeField] public List<GameEventConfig> GameEvents { get; private set; }
    }
}