using SpaceRogue.Abstraction;
using System;

namespace SpaceRogue.Gameplay.Shooting
{
    public sealed class UnitWeapon : IDisposable
    {

        #region Fields

        private readonly IUnitWeaponInput _input;

        #endregion


        #region Properties

        public bool IsEnable { get; set; } = true;

        public MountedWeapon MountedWeapon { get; private set; }

        #endregion


        #region CodeLife

        public UnitWeapon(MountedWeapon mountedWeapon, IUnitWeaponInput input)
        {
            MountedWeapon = mountedWeapon;
            _input = input;

            _input.PrimaryFireInput += HandleFiringInput;
        }

        public void Dispose()
        {
            _input.PrimaryFireInput -= HandleFiringInput;

            MountedWeapon.Weapon.Dispose();
        }

        #endregion


        #region Methods

        private void HandleFiringInput(bool buttonIsPressed)
        {
            if (buttonIsPressed && IsEnable)
            {
                MountedWeapon.CommenceFiring();
            }
        }

        #endregion

    }
}