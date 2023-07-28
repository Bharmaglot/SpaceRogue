using SpaceRogue.Abstraction;
using SpaceRogue.Gameplay.Abilities;
using System;
using UnityEngine;

namespace SpaceRogue.Gameplay.Shooting
{
    public sealed class UnitWeapon : IDisposable
    {

        #region Events

        public event Action UnitWeaponChanged; //TODO Change Weapon

        #endregion


        #region Fields

        private readonly MountedWeapon _mountedWeapon;
        private readonly Ability _ability;
        private readonly IUnitWeaponInput _input;

        #endregion


        #region Properties

        public Weapon CurrentWeapon { get; private set; }
        public Ability CurrentAbility { get; private set; }

        #endregion


        #region CodeLife

        public UnitWeapon(MountedWeapon mountedWeapon, Ability ability, IUnitWeaponInput input)
        {
            _mountedWeapon = mountedWeapon;
            _ability = ability;
            _input = input;

            CurrentWeapon = _mountedWeapon.Weapon;
            CurrentAbility = _ability;

            _input.PrimaryFireInput += HandleFiringInput;
            _input.AbilityInput += AbilityInput;
            _input.ChangeWeaponInput += ChangeWeaponInputHandler;
        }

        public void Dispose()
        {
            _input.PrimaryFireInput -= HandleFiringInput;
            _input.AbilityInput -= AbilityInput;
            _input.ChangeWeaponInput -= ChangeWeaponInputHandler;

            CurrentWeapon.Dispose();
            CurrentAbility.Dispose();
        }

        #endregion


        #region Methods

        private void HandleFiringInput(bool buttonIsPressed)
        {
            if (buttonIsPressed)
            {
                _mountedWeapon.CommenceFiring();
            }
        }
        
        private void AbilityInput(bool buttonIsPressed)
        {
            if (buttonIsPressed)
            {
                _ability.UseAbility();
            }
        }

        private void ChangeWeaponInputHandler(bool isNextWeapon)
        {

            UnitWeaponChanged?.Invoke();
            Debug.Log(isNextWeapon ? "next" : "prev");

        }

        #endregion

    }
}