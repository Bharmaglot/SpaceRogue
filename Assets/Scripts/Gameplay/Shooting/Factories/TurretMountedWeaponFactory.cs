using SpaceRogue.Abstraction;
using SpaceRogue.Gameplay.Shooting.Scriptables;
using Zenject;


namespace SpaceRogue.Gameplay.Shooting.Factories
{
    public sealed class TurretMountedWeaponFactory : PlaceholderFactory<Weapon, EntityViewBase, TurretViewFactory, GunPointViewFactory, TurretConfig, TurretMountedWeapon>
    {
    }
}