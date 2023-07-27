using SpaceRogue.Enums;
using UnityEngine;


namespace SpaceRogue.Gameplay.Shooting.Scriptables
{
    [CreateAssetMenu(fileName = nameof(MinigunConfig), menuName = "Configs/Weapons/" + nameof(MinigunConfig))]
    public sealed class MinigunConfig : WeaponConfig
    {
        [field: SerializeField] public ProjectileConfig MinigunProjectile { get; private set; }
        [field: SerializeField, Range(0.0f, 180.0f)] public float SprayAngle { get; internal set; } = 1.0f;
        [field: SerializeField, Range(0.0f, 180.0f)] public float MaxSprayAngle { get; private set; } = 20.0f;
        [field: SerializeField, Min(0.1f)] public float OverheatCoolDown { get; private set; } = 2.0f;
        [field: SerializeField, Min(1)] public int TimeToOverheat { get; private set; } = 5;

        public MinigunConfig()
        {
            Type = WeaponType.Minigun;
        }
    }
}