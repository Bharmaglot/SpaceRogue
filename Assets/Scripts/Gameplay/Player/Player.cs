using Gameplay.Movement;
using Gameplay.Survival;
using SpaceRogue.Gameplay.Shooting;
using SpaceRogue.InputSystem;
using SpaceRogue.Player.Movement;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace SpaceRogue.Gameplay.Player
{
    public sealed class Player : IDisposable
    {

        #region Events

        public event Action PlayerDestroyed;
        public event Action PlayerDisposed;

        public event Action<UnitWeapon> OnWeaponChange;

        #endregion


        #region Fields

        private int _currentWeaponID;

        private readonly UnitMovement _unitMovement;
        private readonly UnitTurningMouse _unitTurningMouse;
        private readonly List<UnitWeapon> _unitWeapons;
        private readonly PlayerInput _playerInput;

        private bool _disposing;

        #endregion


        #region Properties

        public PlayerView PlayerView { get; }
        public EntitySurvival Survival { get; }
        public UnitWeapon CurrentWeapon => _unitWeapons[_currentWeaponID];

        #endregion


        #region CodeLife

        public Player(
            PlayerView playerView,
            UnitMovement unitMovement,
            UnitTurningMouse unitTurningMouse,
            EntitySurvival playerSurvival,
            List<UnitWeapon> unitWeapon,
            PlayerInput playerInput)
        {
            PlayerView = playerView;
            _unitMovement = unitMovement;
            _unitTurningMouse = unitTurningMouse;
            _unitWeapons = unitWeapon;
            _currentWeaponID = _unitWeapons.Count - 1;
            _playerInput = playerInput;
            Survival = playerSurvival;

            Survival.UnitDestroyed += OnDeath;
            _playerInput.ChangeWeaponInput += ChangeWeaponInputHandler;
        }

        public void Dispose()
        {
            if (_disposing)
            {
                return;
            }

            _disposing = true;
            Survival.UnitDestroyed -= OnDeath;
            _playerInput.ChangeWeaponInput -= ChangeWeaponInputHandler;

            PlayerDisposed?.Invoke();

            Survival.Dispose();
            _unitMovement.Dispose();
            _unitTurningMouse.Dispose();

            foreach (var weapon in _unitWeapons)
            {
                weapon.Dispose();
            }

            if (PlayerView != null)
            {
                UnityEngine.Object.Destroy(PlayerView.gameObject);
            }
        }

        #endregion


        #region Metods

        public void SetStartPosition(Vector2 position)
        {
            _unitMovement.StopMoving();
            PlayerView.transform.position = position;
        }

        private void OnDeath()
        {
            PlayerDestroyed?.Invoke();
            Dispose();
        }

        private void ChangeWeaponInputHandler(bool isNextWeapon)
        {
            _unitWeapons[_currentWeaponID].IsEnable = false;

            if (isNextWeapon)
            {
                _currentWeaponID++;

                if (_currentWeaponID == _unitWeapons.Count)
                {
                    _currentWeaponID = 0;
                }
            }
            else
            {
                _currentWeaponID--;

                if (_currentWeaponID < 0)
                {
                    _currentWeaponID = _unitWeapons.Count - 1;
                }
            }

            _unitWeapons[_currentWeaponID].IsEnable = true;

            OnWeaponChange?.Invoke(_unitWeapons[_currentWeaponID]);
        }

        #endregion

    }
}