using SpaceRogue.Enums;
using UnityEngine;


namespace SpaceRogue.Gameplay.Abilities.Scriptables
{
    [CreateAssetMenu(fileName = nameof(BlasterAbilityConfig), menuName = "Configs/Abilities/" + nameof(BlasterAbilityConfig))]
    public sealed class BlasterAbilityConfig : AbilityConfig
    {
        //MinePrefab & Config

        public BlasterAbilityConfig()
        {
            Type = AbilityType.BlasterAbility;
        }
    }
}