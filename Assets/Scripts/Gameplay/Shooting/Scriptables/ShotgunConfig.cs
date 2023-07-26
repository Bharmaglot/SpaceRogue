using SpaceRogue.Enums;
using UnityEngine;


namespace SpaceRogue.Gameplay.Shooting.Scriptables
{
    [CreateAssetMenu(fileName = nameof(ShotgunConfig), menuName = "Configs/Weapons/" + nameof(ShotgunConfig))]
    public sealed class ShotgunConfig : WeaponConfig
    {
        [field: SerializeField] public ProjectileConfig ShotgunProjectile { get; private set; }
        [field: SerializeField, Min(1)] public int PelletCount { get; private set; } = 3;
        [field: SerializeField, Range(0, 180)] public int SprayAngle { get; private set; } = 20;

        public ShotgunConfig()
        {
            Type = WeaponType.Shotgun;
        }
    }
}