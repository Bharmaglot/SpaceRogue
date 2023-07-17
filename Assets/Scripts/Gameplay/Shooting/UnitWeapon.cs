using SpaceRogue.Abstraction;
using System;


namespace Gameplay.Shooting
{
    public sealed class UnitWeapon : IDisposable
    {
        private readonly MountedWeapon _mountedWeapon;
        private readonly IUnitWeaponInput _input;

        public UnitWeapon(MountedWeapon mountedWeapon, IUnitWeaponInput input)
        {
            _mountedWeapon = mountedWeapon;
            _input = input;
            _input.PrimaryFireInput += HandleFiringInput;
        }
        
        public void Dispose()
        {
            _input.PrimaryFireInput -= HandleFiringInput;
        }

        private void HandleFiringInput(bool buttonIsPressed)
        {
            if (buttonIsPressed)
            {
                _mountedWeapon.CommenceFiring();
            }
        }
    }
}