using SpaceRogue.Enums;
using UnityEngine;


namespace SpaceRogue.Gameplay.Abilities.Scriptables
{
    [CreateAssetMenu(fileName = nameof(RailgunAbilityConfig), menuName = "Configs/Abilities/" + nameof(RailgunAbilityConfig))]
    public sealed class RailgunAbilityConfig : AbilityConfig
    {
        //CircularWavePrefab & Config

        public RailgunAbilityConfig()
        {
            Type = AbilityType.RailgunAbility;
        }
    }
}