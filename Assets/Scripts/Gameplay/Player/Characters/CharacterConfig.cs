using Gameplay.Survival;
using SpaceRogue.Gameplay.Abilities.Scriptables;
using SpaceRogue.Gameplay.Shooting.Scriptables;
using UnityEngine;


namespace SpaceRogue.Gameplay.Player.Character
{
    [CreateAssetMenu(fileName = nameof(CharacterConfig), menuName = "Configs/Player/" + nameof(CharacterConfig))]
    public sealed class CharacterConfig : ScriptableObject
    {
        [field: SerializeField] public Sprite CharacterIcon { get; private set; }
        [field: SerializeField] public Sprite SpaceshipSprite { get; private set; }
        [field: SerializeField] public EntitySurvivalConfig Survival { get; private set; }
        [field: SerializeField] public MountedWeaponConfig MountedWeapon { get; private set; }
        [field: SerializeField] public AbilityConfig Ability { get; private set; }

    }
}