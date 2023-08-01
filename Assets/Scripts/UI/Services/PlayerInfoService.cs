using SpaceRogue.Gameplay.Abilities;
using SpaceRogue.Gameplay.Player;
using SpaceRogue.Gameplay.Player.Character;
using SpaceRogue.Gameplay.Shooting.Weapons;
using SpaceRogue.UI.Game;
using System;


namespace SpaceRogue.UI.Services
{
    public sealed class PlayerInfoService : IDisposable
    {

        #region Fields

        private readonly PlayerUsedItemView _playerWeaponView;
        private readonly PlayerUsedItemView _playerAbilityView;
        private readonly PlayerFactory _playerFactory;
        
        private Gameplay.Player.Player _player;

        private Character _currentCharacter;

        #endregion


        #region CodeLife

        public PlayerInfoService(PlayerInfoView playerInfoView, PlayerFactory playerFactory)
        {
            _playerWeaponView = playerInfoView.PlayerWeaponView;
            _playerAbilityView = playerInfoView.PlayerAbilityView;
            _playerFactory = playerFactory;

            _playerFactory.OnPlayerSpawned += OnPlayerSpawnedHandler;

            _playerWeaponView.Hide();
            _playerAbilityView.Hide();
        }

        public void Dispose()
        {
            _playerFactory.OnPlayerSpawned -= OnPlayerSpawnedHandler;
            _player.OnCharacterChange -= OnCharacterChanged;

            UnsubscribesFromWeaponsAndAbilities();
        }

        #endregion


        #region Methods

        private void OnPlayerSpawnedHandler(Gameplay.Player.Player player)
        {
            if (_player != null)
            {
                _player.OnCharacterChange -= OnCharacterChanged;
            }

            _player = player;
            _player.OnCharacterChange += OnCharacterChanged;

            OnCharacterChanged(_player.CurrentCharacter);
        }

        private void OnCharacterChanged(Character character)
        {
            UnsubscribesFromWeaponsAndAbilities();

            _currentCharacter = character;

            SubscriptionsForWeaponsAndAbilities();
            SetupWeaponsAndAbilities();
        }

        private void UnsubscribesFromWeaponsAndAbilities()
        {
            if (_currentCharacter != null)
            {
                _currentCharacter.UnitWeapon.MountedWeapon.Weapon.WeaponAvailable -= OnWeaponAvailable;
                _currentCharacter.UnitWeapon.MountedWeapon.Weapon.WeaponUsed -= OnWeaponUsed;
                _currentCharacter.UnitAbility.Ability.AbilityAvailable -= OnAbilityAvailable;
                _currentCharacter.UnitAbility.Ability.AbilityUsed -= OnAbilityUsed; 
            }
        }

        private void SubscriptionsForWeaponsAndAbilities()
        {
            _currentCharacter.UnitWeapon.MountedWeapon.Weapon.WeaponAvailable += OnWeaponAvailable;
            _currentCharacter.UnitWeapon.MountedWeapon.Weapon.WeaponUsed += OnWeaponUsed;
            _currentCharacter.UnitAbility.Ability.AbilityAvailable += OnAbilityAvailable;
            _currentCharacter.UnitAbility.Ability.AbilityUsed += OnAbilityUsed;
        }

        private void SetupWeaponsAndAbilities()
        {
            if (_currentCharacter.UnitWeapon.MountedWeapon.Weapon is NullGun)
            {
                _playerWeaponView.Hide();
            }
            else
            {
                _playerWeaponView.Show();
                _playerWeaponView.Init(_currentCharacter.UnitWeapon.MountedWeapon.Weapon.WeaponName);
                _playerWeaponView.SetPanelActive(true);
            }

            if (_currentCharacter.UnitAbility.Ability is NullAbility)
            {
                _playerAbilityView.Hide();
            }
            else
            {
                _playerAbilityView.Show();
                _playerAbilityView.Init(_currentCharacter.UnitAbility.Ability.AbilityName);
                _playerAbilityView.SetPanelActive(true);
            }
        }

        private void OnWeaponAvailable() => _playerWeaponView.SetPanelActive(true);

        private void OnWeaponUsed() => _playerWeaponView.SetPanelActive(false);

        private void OnAbilityAvailable() => _playerAbilityView.SetPanelActive(true);

        private void OnAbilityUsed() => _playerAbilityView.SetPanelActive(false);

        #endregion

    }
}