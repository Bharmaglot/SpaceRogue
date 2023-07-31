using Gameplay.Movement;
using SpaceRogue.Gameplay.Player.Character;
using System.Collections.Generic;
using UnityEngine;


namespace SpaceRogue.Scriptables
{
    [CreateAssetMenu(fileName = nameof(PlayerConfig), menuName = "Configs/Player/" + nameof(PlayerConfig))]
    public sealed class PlayerConfig : ScriptableObject
    {
        [field: SerializeField] public List<CharacterConfig> Characters { get; private set; }
        [field: SerializeField] public UnitMovementConfig UnitMovement { get; private set; }

    }
}