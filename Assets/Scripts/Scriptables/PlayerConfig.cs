using Gameplay.Movement;
using Gameplay.Survival;
using SpaceRogue.Gameplay.Shooting.Scriptables;
using System.Collections.Generic;
using UnityEngine;


namespace SpaceRogue.Scriptables
{
    [CreateAssetMenu(fileName = nameof(PlayerConfig), menuName = "Configs/Player/" + nameof(PlayerConfig))]
    public sealed class PlayerConfig : ScriptableObject
    {

        [field: SerializeField] public List<MountedWeaponConfig> AvailableWeapons { get; private set; }
        [field: SerializeField] public UnitMovementConfig UnitMovement { get; private set; }
        [field: SerializeField] public EntitySurvivalConfig Survival { get; private set; }

    }
}