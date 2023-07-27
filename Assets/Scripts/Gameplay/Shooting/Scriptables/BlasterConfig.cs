using SpaceRogue.Enums;
using UnityEngine;


namespace SpaceRogue.Gameplay.Shooting.Scriptables
{
    [CreateAssetMenu(fileName = nameof(BlasterConfig), menuName = "Configs/Weapons/" + nameof(BlasterConfig))]
    public sealed class BlasterConfig : WeaponConfig
    {
        [field: SerializeField] public ProjectileConfig BlasterProjectile { get; private set; }
        [field: SerializeField, Range(0, 180)] public int SprayAngle { get; private set; } = 0;

        public BlasterConfig()
        {
            Type = WeaponType.Blaster;
        }
    }
}