using SpaceRogue.Enums;
using SpaceRogue.Gameplay.Shooting.Scriptables;
using UnityEngine;


namespace SpaceRogue.Gameplay.Abilities.Scriptables
{
    [CreateAssetMenu(fileName = nameof(TorpedoAbilityConfig), menuName = "Configs/Abilities/" + nameof(TorpedoAbilityConfig))]
    public class TorpedoAbilityConfig : AbilityConfig
    {
            [field: SerializeField] public TorpedoConfig TorpedoConfig { get; private set; }

        public TorpedoAbilityConfig()
        {
            Type = AbilityType.TorpedoAbility;
        }
    }
}
