using Gameplay.Space.SpaceObjects.Scriptables;
using SpaceRogue.Enums;
using UnityEngine;


namespace SpaceRogue.Gameplay.Abilities.Scriptables
{
    [CreateAssetMenu(fileName = nameof(ShotgunAbilityConfig), menuName = "Configs/Abilities/" + nameof(ShotgunAbilityConfig))]
    public sealed class ShotgunAbilityConfig : AbilityConfig
    {
        //AreaOfAttractionPrefab & Config

        [field: SerializeField] public GravitationAuraConfig GravitaionAreaConfig { get; private set; }
        [field: SerializeField] public float LifeTime { get; private set; } = 3.0f;
        [field: SerializeField] public float Distance { get; private set; } = 5.0f;


        public ShotgunAbilityConfig()
        {
            Type = AbilityType.ShotgunAbility;
        }
    }
}