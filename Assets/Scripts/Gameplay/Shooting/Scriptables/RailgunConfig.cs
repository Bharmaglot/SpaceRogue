using SpaceRogue.Enums;
using UnityEngine;


namespace SpaceRogue.Gameplay.Shooting.Scriptables
{
    [CreateAssetMenu(fileName = nameof(RailgunConfig), menuName = "Configs/Weapons/" + nameof(RailgunConfig))]
    public sealed class RailgunConfig : WeaponConfig
    {
        [field: SerializeField] public ProjectileConfig RailgunProjectile { get; private set; }

        public RailgunConfig()
        {
            Type = WeaponType.Railgun;
        }
    }
}