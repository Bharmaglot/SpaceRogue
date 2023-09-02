using SpaceRogue.Enums;
using UnityEngine;


namespace SpaceRogue.Gameplay.Shooting.Scriptables
{
    [CreateAssetMenu(fileName = nameof(RocketLauncherConfig), menuName = "Configs/Weapons/" + nameof(RocketLauncherConfig))]
    public sealed class RocketLauncherConfig : WeaponConfig
    {
        [field: SerializeField] public RocketConfig RocketConfig;
        public RocketLauncherConfig()
        {
            Type = WeaponType.RocketLauncher;
        }
    }
}