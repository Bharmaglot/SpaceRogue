using SpaceRogue.Enums;
using UnityEngine;


namespace SpaceRogue.Gameplay.Abilities.Scriptables
{
    [CreateAssetMenu(fileName = nameof(ShotgunAbilityConfig), menuName = "Configs/Abilities/" + nameof(ShotgunAbilityConfig))]
    public sealed class ShotgunAbilityConfig : AbilityConfig
    {
        //AreaOfAttractionPrefab & Config

        public ShotgunAbilityConfig()
        {
            Type = AbilityType.ShotgunAbility;
        }
    }
}