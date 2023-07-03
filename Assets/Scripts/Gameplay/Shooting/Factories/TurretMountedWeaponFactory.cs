using Gameplay.Shooting.Scriptables;
using SpaceRogue.Abstraction;
using Zenject;


namespace Gameplay.Shooting.Factories
{
    public class TurretMountedWeaponFactory : PlaceholderFactory<Weapon, EntityViewBase, TurretViewFactory, GunPointViewFactory, TurretConfig, TurretMountedWeapon>
    {
    }
}