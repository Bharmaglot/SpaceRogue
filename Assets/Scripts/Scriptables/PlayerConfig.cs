using Gameplay.Movement;
using SpaceRogue.Gameplay.Shooting.Scriptables;
using Gameplay.Survival;
using UnityEngine;

namespace Scriptables
{
    [CreateAssetMenu(fileName = nameof(PlayerConfig), menuName = "Configs/Player/" + nameof(PlayerConfig))]
    public sealed class PlayerConfig : ScriptableObject
    {
        [field: SerializeField] public MountedWeaponConfig StartingWeapon { get; private set; }
        [field: SerializeField] public UnitMovementConfig UnitMovement { get; private set; }
        [field: SerializeField] public EntitySurvivalConfig Survival { get; private set; }
    }
}