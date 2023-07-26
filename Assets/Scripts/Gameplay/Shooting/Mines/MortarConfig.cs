using UnityEngine;


namespace Gameplay.Shooting.Scriptables
{
    [CreateAssetMenu(fileName = nameof(MortarConfig), menuName = "Configs/Weapons/" + nameof(MortarConfig))]
    public sealed class MortarConfig : WeaponConfig
    {
        [field: SerializeField] public MineConfig MineConfig { get; private set; }
        [field: SerializeField, Range(0, 180)] public int SprayAngle { get; private set; } = 0;
        [field: SerializeField] public int Distance { get; private set; }
        [field: SerializeField] public int StartSpeed { get; private set; }
        public MortarConfig() => Type = WeaponType.Mortar;
    }
}