using UnityEngine;

namespace Gameplay.Shooting.Scriptables
{
    [CreateAssetMenu(fileName = nameof(MountedWeaponConfig), menuName = "Configs/Weapons/" + nameof(MountedWeaponConfig))]
    public class MountedWeaponConfig : ScriptableObject
    {
        [field: SerializeField] public WeaponMountType WeaponMountType { get; private set; } = WeaponMountType.None;
        [field: SerializeField] public WeaponConfig MountedWeapon { get; private set; }
        [field: SerializeField] public TurretConfig TurretConfig { get; private set; }
    }
}