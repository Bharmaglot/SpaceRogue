using SpaceRogue.Enums;
using SpaceRogue.Gameplay.Shooting.Scriptables;
using UnityEngine;


namespace SpaceRogue.Gameplay.Abilities.Scriptables
{
    [CreateAssetMenu(fileName = nameof(BlasterAbilityConfig), menuName = "Configs/Abilities/" + nameof(BlasterAbilityConfig))]
    public sealed class BlasterAbilityConfig : AbilityConfig
    {
        
        [field: SerializeField] public MineConfig MineConfig { get; private set; }

        public BlasterAbilityConfig()
        {
            Type = AbilityType.BlasterAbility;
        }
    }
}