using System;
using Gameplay.Movement;
using SpaceRogue.Gameplay.Shooting;
using Gameplay.Survival;
using SpaceRogue.Player.Movement;
using System.Collections.Generic;
using SpaceRogue.InputSystem;
using UnityEngine;

namespace Gameplay.Player
{
    public sealed class Player : IDisposable
    {

        private int _currentWeaponID;

        private readonly UnitMovement _unitMovement;
        private readonly UnitTurningMouse _unitTurningMouse;
        private readonly List<UnitWeapon> _unitWeapons;
        private readonly PlayerInput _playerInput;

        public event Action PlayerDestroyed;
        public event Action PlayerDisposed;

        public PlayerView PlayerView { get; }
        public EntitySurvival Survival { get; }

        private bool _disposing;

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

            if (PlayerView is not null)
            {
                UnityEngine.Object.Destroy(PlayerView.gameObject);
            }
        }

        private void OnDeath()
        {
            PlayerDestroyed?.Invoke();
            Dispose();
        }

        private void ChangeWeaponInputHandler(bool isNextWeapon)
        {
            Debug.Log(isNextWeapon?"next":"prev");
            Debug.Log($"disable [{_currentWeaponID}] {_unitWeapons[_currentWeaponID].CurrentWeapon.WeaponName}");

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
            Debug.Log($"enable [{_currentWeaponID}] {_unitWeapons[_currentWeaponID].CurrentWeapon.WeaponName}");
        }
    }
}