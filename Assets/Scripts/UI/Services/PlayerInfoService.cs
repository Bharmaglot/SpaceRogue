using Gameplay.Player;
using SpaceRogue.Gameplay.Abilities;
using SpaceRogue.Gameplay.Shooting;
using SpaceRogue.Gameplay.Shooting.Weapons;
using System;
using UI.Game;
using UnityEngine;

namespace SpaceRogue.UI.Services
{
    public sealed class PlayerInfoService : IDisposable
    {

        #region Fields

        private readonly PlayerUsedItemView _playerWeaponView;
        private readonly PlayerUsedItemView _playerAbilityView;
        private readonly CharacterView _characterView;
        private readonly PlayerFactory _playerFactory;
        private global::Gameplay.Player.Player _player;

        private Sprite _currentCharacterIcon;
        private Weapon _currentWeapon;
        private Ability _currentAbility;

        #endregion


        #region CodeLife

        public PlayerInfoService(PlayerInfoView playerInfoView, PlayerFactory playerFactory)
        {
            _playerWeaponView = playerInfoView.PlayerWeaponView;
            _playerAbilityView = playerInfoView.PlayerAbilityView;
            _characterView = playerInfoView.CharacterView;
            _playerFactory = playerFactory;

            _playerFactory.OnPlayerSpawned += OnPlayerSpawnedHandler;

            _playerWeaponView.Hide();
            _playerAbilityView.Hide();
            _characterView.Hide();
        }

        private void OnPlayerSpawnedHandler(global::Gameplay.Player.Player player)
        {
            _playerFactory.OnPlayerSpawned -= OnPlayerSpawnedHandler;
            _player = player;

            _player.OnWeaponChange += OnUnitWeaponChanged;

            OnUnitWeaponChanged(_player.CurrentWeapon);
        }

        public void Dispose()
        {
            _player.OnWeaponChange -= OnUnitWeaponChanged;
        }

        #endregion


        #region Methods


        private void OnUnitWeaponChanged(UnitWeapon unitWeapon)
        {
            if (_currentWeapon != null)
            {
                UnsubscribesFromWeaponsAndAbilities();
            }

            _currentCharacterIcon = unitWeapon.CharacterIcon != null ? unitWeapon.CharacterIcon : default;
            _currentWeapon = unitWeapon.CurrentWeapon;
            _currentAbility = unitWeapon.CurrentAbility;

            SubscriptionsForWeaponsAndAbilities();
            SetupWeaponsAndAbilities();
        }

        private void UnsubscribesFromWeaponsAndAbilities()
        {
            _currentWeapon.WeaponAvailable -= OnWeaponAvailable;
            _currentWeapon.WeaponUsed -= OnWeaponUsed;
            _currentAbility.AbilityAvailable -= OnAbilityAvailable;
            _currentAbility.AbilityUsed -= OnAbilityUsed;
        }

        private void SubscriptionsForWeaponsAndAbilities()
        {
            _currentWeapon.WeaponAvailable += OnWeaponAvailable;
            _currentWeapon.WeaponUsed += OnWeaponUsed;
            _currentAbility.AbilityAvailable += OnAbilityAvailable;
            _currentAbility.AbilityUsed += OnAbilityUsed;
        }

        private void SetupWeaponsAndAbilities()
        {
            _characterView.Show();
            _characterView.Image.sprite = _currentCharacterIcon;

            if (_currentWeapon is NullGun)
            {
                _playerWeaponView.Hide();
            }
            else
            {
                _playerWeaponView.Show();
                _playerWeaponView.Init(_currentWeapon.WeaponName);
                _playerWeaponView.Panel.color = _playerWeaponView.ColorActive;
            }

            if (_currentAbility is NullAbility)
            {
                _playerAbilityView.Hide();
            }
            else
            {
                _playerAbilityView.Show();
                _playerAbilityView.Init(_currentAbility.AbilityName);
                _playerAbilityView.Panel.color = _playerAbilityView.ColorActive;
            }
        }

        private void OnWeaponAvailable() => _playerWeaponView.Panel.color = _playerWeaponView.ColorActive;

        private void OnWeaponUsed() => _playerWeaponView.Panel.color = _playerWeaponView.ColorNotActive;

        private void OnAbilityAvailable() => _playerAbilityView.Panel.color = _playerAbilityView.ColorActive;

        private void OnAbilityUsed() => _playerAbilityView.Panel.color = _playerAbilityView.ColorNotActive;

        #endregion

    }
}