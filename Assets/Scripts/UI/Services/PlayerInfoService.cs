using Gameplay.Player;
using SpaceRogue.Gameplay.Abilities;
using SpaceRogue.Gameplay.Shooting;
using SpaceRogue.Gameplay.Shooting.Weapons;
using System;
using UI.Game;


namespace SpaceRogue.UI.Services
{
    public sealed class PlayerInfoService : IDisposable
    {

        #region Fields

        private readonly PlayerUsedItemView _playerWeaponView;
        private readonly PlayerUsedItemView _playerAbilityView;
        private readonly PlayerWeaponFactory _playerWeaponFactory;

        private UnitWeapon _unitWeapon;
        private Weapon _currentWeapon;
        private Ability _currentAbility;

        #endregion


        #region CodeLife

        public PlayerInfoService(PlayerInfoView playerInfoView, PlayerWeaponFactory playerWeaponFactory)
        {
            _playerWeaponView = playerInfoView.PlayerWeaponView;
            _playerAbilityView = playerInfoView.PlayerAbilityView;
            _playerWeaponFactory = playerWeaponFactory;

            _playerWeaponView.Hide();
            _playerAbilityView.Hide();

            _playerWeaponFactory.UnitWeaponCreated += OnUnitWeaponCreated;
        }

        public void Dispose()
        {
            UnsubscribeFromAllUnitWeaponEvents();
            _playerWeaponFactory.UnitWeaponCreated -= OnUnitWeaponCreated;
        }

        #endregion


        #region Methods

        private void OnUnitWeaponCreated(UnitWeapon unitWeapon)
        {
            UnsubscribeFromAllUnitWeaponEvents();

            _unitWeapon = unitWeapon;
            _currentWeapon = unitWeapon.CurrentWeapon;
            _currentAbility = unitWeapon.CurrentAbility;

            _unitWeapon.OnUnitWeaponChanged += OnUnitWeaponChanged;
            SubscriptionsForWeaponsAndAbilities();
            SetupWeaponsAndAbilities();
        }

        private void UnsubscribeFromAllUnitWeaponEvents()
        {
            if (_unitWeapon != null)
            {
                _unitWeapon.OnUnitWeaponChanged -= OnUnitWeaponChanged;
                UnsubscribesFromWeaponsAndAbilities();
            }
        }

        private void UnsubscribesFromWeaponsAndAbilities()
        {
            _currentWeapon.WeaponAvailable -= OnWeaponAvailable;
            _currentWeapon.WeaponUsed -= OnWeaponUsed;
            _currentAbility.AbilityAvailable -= OnAbilityAvailable;
            _currentAbility.AbilityUsed -= OnAbilityUsed;
        }

        private void OnUnitWeaponChanged()
        {
            UnsubscribesFromWeaponsAndAbilities();
            _currentWeapon = _unitWeapon.CurrentWeapon;
            _currentAbility = _unitWeapon.CurrentAbility;
            SubscriptionsForWeaponsAndAbilities();
            SetupWeaponsAndAbilities();
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