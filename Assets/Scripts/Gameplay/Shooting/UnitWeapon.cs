using SpaceRogue.Abstraction;
using System;


namespace SpaceRogue.Gameplay.Shooting
{
    public sealed class UnitWeapon : IDisposable
    {
        #region Fields

        private readonly MountedWeapon _mountedWeapon;
        private readonly IUnitWeaponInput _input;

        #endregion

        #region CodeLife

        public UnitWeapon(MountedWeapon mountedWeapon, IUnitWeaponInput input)
        {
            _mountedWeapon = mountedWeapon;
            _input = input;
            _input.PrimaryFireInput += HandleFiringInput;
        }

        public void Dispose() => _input.PrimaryFireInput -= HandleFiringInput;

        #endregion

        #region Methods

        private void HandleFiringInput(bool buttonIsPressed)
        {
            if (buttonIsPressed)
            {
                _mountedWeapon.CommenceFiring();
            }
        }

        #endregion
    }
}