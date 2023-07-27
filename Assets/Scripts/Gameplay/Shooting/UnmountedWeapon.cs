using SpaceRogue.Abstraction;


namespace SpaceRogue.Gameplay.Shooting
{
    public sealed class UnmountedWeapon : MountedWeapon
    {
        public UnmountedWeapon(Weapon weapon, EntityViewBase entityView) : base(weapon, entityView) { }

        public override void CommenceFiring() { }
    }
}