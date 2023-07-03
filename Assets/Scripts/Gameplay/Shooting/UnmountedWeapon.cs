using SpaceRogue.Abstraction;


namespace Gameplay.Shooting
{
    public class UnmountedWeapon : MountedWeapon
    {
        public UnmountedWeapon(Weapon weapon, EntityViewBase entityView) : base(weapon, entityView) { }

        public override void CommenceFiring()
        {
        }
    }
}