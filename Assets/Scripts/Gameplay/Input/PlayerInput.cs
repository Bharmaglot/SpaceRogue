using SpaceRogue.Abstraction;
using SpaceRogue.Services;
using System;
using UnityEngine;


namespace SpaceRogue.InputSystem
{
    public sealed class PlayerInput : IDisposable, IUnitMovementInput, IUnitTurningMouseInput, IUnitWeaponInput
    {

        #region Events

        public event Action<Vector3> MousePositionInput;

        public event Action<float> VerticalAxisInput;
        public event Action<float> HorizontalAxisInput;

        public event Action<bool> PrimaryFireInput;
        public event Action<bool> ChangeWeaponInput;
        public event Action<bool> AbilityInput;
        public event Action<bool> NextLevelInput;
        public event Action<bool> MapInput;

        #endregion


        #region Fields

        private const string VERTICAL = "Vertical";
        private const string HORIZONTAL = "Horizontal";
        private const KeyCode PRIMARY_FIRE = KeyCode.Mouse0;
        private const KeyCode ABILITY = KeyCode.Mouse1;
        private const KeyCode CHANGE_WEAPON = KeyCode.Q;
        private const KeyCode NEXT_LEVEL = KeyCode.G;
        private const KeyCode MAP = KeyCode.Tab;

        private readonly Updater _updater;
        private readonly PlayerInputConfig _playerInputConfig;

        #endregion


        #region CodeLife

        public PlayerInput(Updater updater, PlayerInputConfig playerInputConfig)
        {
            _updater = updater;
            _playerInputConfig = playerInputConfig;

            _updater.SubscribeToUpdate(CheckVerticalInput);
            _updater.SubscribeToUpdate(CheckHorizontalInput);
            _updater.SubscribeToUpdate(CheckFiringInput);
            _updater.SubscribeToUpdate(CheckAbilityInput);
            _updater.SubscribeToUpdate(CheckMousePositionInput);
            _updater.SubscribeToUpdate(CheckChangeWeaponInput);
            _updater.SubscribeToUpdate(CheckNextLevelInput);
            _updater.SubscribeToUpdate(CheckMapInput);
        }

        public void Dispose()
        {
            _updater.UnsubscribeFromUpdate(CheckVerticalInput);
            _updater.UnsubscribeFromUpdate(CheckHorizontalInput);
            _updater.UnsubscribeFromUpdate(CheckFiringInput);
            _updater.UnsubscribeFromUpdate(CheckAbilityInput);
            _updater.UnsubscribeFromUpdate(CheckMousePositionInput);
            _updater.UnsubscribeFromUpdate(CheckChangeWeaponInput);
            _updater.UnsubscribeFromUpdate(CheckNextLevelInput);
            _updater.UnsubscribeFromUpdate(CheckMapInput);
        }

        #endregion


        #region Methods

        private void CheckVerticalInput()
        {
            float verticalOffset = Input.GetAxis(VERTICAL);
            float inputValue = CalculateInputValue(verticalOffset, _playerInputConfig.VerticalInputMultiplier);
            VerticalAxisInput?.Invoke(inputValue);
        }

        private void CheckHorizontalInput()
        {
            float horizontalOffset = Input.GetAxis(HORIZONTAL);
            float inputValue = CalculateInputValue(horizontalOffset, _playerInputConfig.HorizontalInputMultiplier);
            HorizontalAxisInput?.Invoke(inputValue);
        }

        private void CheckFiringInput()
        {
            bool value = Input.GetKey(PRIMARY_FIRE);
            PrimaryFireInput?.Invoke(value);
        }
        
        private void CheckAbilityInput()
        {
            bool value = Input.GetKey(ABILITY);
            AbilityInput?.Invoke(value);
        }

        private void CheckMousePositionInput()
        {
            Vector3 value = Input.mousePosition;
            MousePositionInput?.Invoke(value);
        }

        private void CheckChangeWeaponInput()
        {
            bool value = Input.GetKeyDown(CHANGE_WEAPON);
            ChangeWeaponInput?.Invoke(value);
        }

        private void CheckNextLevelInput()
        {
            bool value = Input.GetKeyDown(NEXT_LEVEL);
            NextLevelInput?.Invoke(value);
        }

        private void CheckMapInput()
        {
            bool value = Input.GetKey(MAP);
            MapInput?.Invoke(value);
        }

        private static float CalculateInputValue(float axisOffset, float inputMultiplier) => axisOffset * inputMultiplier;

        #endregion

    }
}