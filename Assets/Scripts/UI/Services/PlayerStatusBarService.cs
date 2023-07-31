using Gameplay.Survival;
using SpaceRogue.Gameplay.Player;
using SpaceRogue.Gameplay.Player.Character;
using SpaceRogue.UI.Game;
using System;


namespace SpaceRogue.UI.Services
{
    public sealed class PlayerStatusBarService : IDisposable
    {

        #region Fields

        private readonly PlayerStatusBarView _playerStatusBarView;
        private readonly PlayerFactory _playerFactory;

        private Gameplay.Player.Player _player;

        private EntitySurvival _currentSurvival;

        #endregion


        #region CodeLife

        public PlayerStatusBarService(PlayerInfoView playerInfoView, PlayerFactory playerFactory)
        {
            _playerStatusBarView = playerInfoView.PlayerStatusBarView;
            _playerFactory = playerFactory;

            _playerStatusBarView.Hide();

            _playerFactory.OnPlayerSpawned += OnPlayerSpawnedHandler;
        }

        public void Dispose()
        {
            _playerFactory.OnPlayerSpawned -= OnPlayerSpawnedHandler;
            _player.OnCharacterChange -= OnSurvivalChanged;
            UnsubscribesFromEntityHealth();
        }

        #endregion


        #region Methods

        private void OnPlayerSpawnedHandler(Gameplay.Player.Player player)
        {
            if (_player != null)
            {
                _player.OnCharacterChange -= OnSurvivalChanged;
            }

            _player = player;
            _player.OnCharacterChange += OnSurvivalChanged;

            OnSurvivalChanged(_player.CurrentCharacter);
        }


        private void OnSurvivalChanged(Character character)
        {
            UnsubscribesFromEntityHealth();

            _currentSurvival = character.Survival;

            _playerStatusBarView.HealthBar.Init(
                0.0f,
                _currentSurvival.EntityHealth.MaximumHealth,
                _currentSurvival.EntityHealth.CurrentHealth);

            _currentSurvival.EntityHealth.HealthChanged += UpdateHealthBar;

            if (_currentSurvival.EntityShield != null)
            {
                _playerStatusBarView.ShieldBar.Init(
                    0.0f,
                    _currentSurvival.EntityShield.MaximumShield,
                    _currentSurvival.EntityShield.CurrentShield);
                _currentSurvival.EntityShield.ShieldChanged += UpdateShieldBar;
            }

            _playerStatusBarView.Show();
        }

        private void UnsubscribesFromEntityHealth()
        {
            if (_currentSurvival != null)
            {
                _currentSurvival.EntityHealth.HealthChanged -= UpdateHealthBar;

                if (_currentSurvival.EntityShield != null)
                {
                    _currentSurvival.EntityShield.ShieldChanged -= UpdateShieldBar;
                }
            }
        }

        private void UpdateHealthBar() 
            => _playerStatusBarView.HealthBar.UpdateValue(_currentSurvival.EntityHealth.CurrentHealth);

        private void UpdateShieldBar() 
            => _playerStatusBarView.ShieldBar.UpdateValue(_currentSurvival.EntityShield.CurrentShield);

        #endregion

    }
}