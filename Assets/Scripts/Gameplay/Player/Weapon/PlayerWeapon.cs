using System;
using SpaceRogue.Gameplay.Shooting;
using SpaceRogue.InputSystem;


namespace Gameplay.Player.Weapon
{
    public sealed class PlayerWeapon : IDisposable
    {
        private readonly PlayerInput _playerInput;
        private readonly MountedWeapon _mountedWeapon;

        public PlayerWeapon(MountedWeapon mountedWeapon, PlayerInput playerInput)
        {
            _playerInput = playerInput;
            _mountedWeapon = mountedWeapon;

            _playerInput.PrimaryFireInput += HandleFiringInput;
        }

        public void Dispose()
        {
            _playerInput.PrimaryFireInput -= HandleFiringInput;
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