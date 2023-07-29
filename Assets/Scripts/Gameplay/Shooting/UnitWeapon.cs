using SpaceRogue.Abstraction;
using SpaceRogue.Gameplay.Abilities;
using System;
using UnityEngine;

namespace SpaceRogue.Gameplay.Shooting
{
    public sealed class UnitWeapon : IDisposable
    {

        #region Events

        public event Action OnUnitWeaponActivated;

        #endregion


        #region Fields

        private bool _isEnable = true;

        private readonly MountedWeapon _mountedWeapon;
        private readonly Ability _ability;
        private readonly IUnitWeaponInput _input;

        #endregion


        #region Properties

        public bool IsEnable
        {
            get => _isEnable;
            set
            {
                _isEnable = value;

                if (_isEnable)
                {
                    OnUnitWeaponActivated?.Invoke();
                }
            }
        }

        public Sprite CharacterIcon { get; private set; }
        public Weapon CurrentWeapon { get; private set; }
        public Ability CurrentAbility { get; private set; }

        #endregion


        #region CodeLife

        public UnitWeapon(Sprite characterIcon, MountedWeapon mountedWeapon, Ability ability, IUnitWeaponInput input)
        {
            CharacterIcon = characterIcon;
            _mountedWeapon = mountedWeapon;
            _ability = ability;
            _input = input;

            CurrentWeapon = _mountedWeapon.Weapon;
            CurrentAbility = _ability;

            _input.PrimaryFireInput += HandleFiringInput;
            _input.AbilityInput += AbilityInput;
        }

        public void Dispose()
        {
            _input.PrimaryFireInput -= HandleFiringInput;
            _input.AbilityInput -= AbilityInput;

            CurrentWeapon.Dispose();
            CurrentAbility.Dispose();
        }

        #endregion


        #region Methods

        private void HandleFiringInput(bool buttonIsPressed)
        {
            if (buttonIsPressed && IsEnable)
            {
                _mountedWeapon.CommenceFiring();
            }
        }

        private void AbilityInput(bool buttonIsPressed)
        {
            if (buttonIsPressed && IsEnable)
            {
                _ability.UseAbility();
            }
        }

        #endregion

    }
}