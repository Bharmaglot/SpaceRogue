using SpaceRogue.Enums;
using UnityEngine;


namespace SpaceRogue.Gameplay.Abilities.Scriptables
{
    [CreateAssetMenu(fileName = nameof(MinigunAbilityConfig), menuName = "Configs/Abilities/" + nameof(MinigunAbilityConfig))]
    public sealed class MinigunAbilityConfig : AbilityConfig
    {
        //Config

        public MinigunAbilityConfig()
        {
            Type = AbilityType.MinigunAbility;
        }
    }
}