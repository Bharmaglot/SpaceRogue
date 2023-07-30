using Gameplay.Survival;
using SpaceRogue.Gameplay.Player;
using SpaceRogue.UI.Game;
using System;


namespace SpaceRogue.UI.Services
{
    public sealed class PlayerStatusBarService : IDisposable
    {

        #region Fields

        private readonly PlayerStatusBarView _playerStatusBarView;
        private readonly PlayerSurvivalFactory _survivalFactory;

        private EntitySurvival _playerSurvival;

        #endregion


        #region CodeLife

        public PlayerStatusBarService(PlayerInfoView playerInfoView, PlayerSurvivalFactory survivalFactory)
        {
            _playerStatusBarView = playerInfoView.PlayerStatusBarView;
            _survivalFactory = survivalFactory;

            _playerStatusBarView.Hide();

            _survivalFactory.PlayerSurvivalCreated += OnPlayerSurvivalCreated;
        }

        public void Dispose()
        {
            _survivalFactory.PlayerSurvivalCreated -= OnPlayerSurvivalCreated;

            if (_playerSurvival != null)
            {
                _playerSurvival.EntityHealth.HealthChanged -= UpdateHealthBar;

                if (_playerSurvival.EntityShield != null)
                {
                    _playerSurvival.EntityShield.ShieldChanged -= UpdateShieldBar;
                }
            }
        }

        #endregion


        #region Methods
        private void OnPlayerSurvivalCreated(EntitySurvival entitySurvival)
        {
            _playerSurvival = entitySurvival;

            _playerStatusBarView.HealthBar.Init(
                0.0f,
                entitySurvival.EntityHealth.MaximumHealth,
                entitySurvival.EntityHealth.CurrentHealth);

            _playerSurvival.EntityHealth.HealthChanged += UpdateHealthBar;

            if (_playerSurvival.EntityShield != null)
            {
                _playerStatusBarView.ShieldBar.Init(
                    0.0f,
                    entitySurvival.EntityShield.MaximumShield,
                    entitySurvival.EntityShield.CurrentShield);
                _playerSurvival.EntityShield.ShieldChanged += UpdateShieldBar;
            }

            _playerStatusBarView.Show();
        }

        private void UpdateHealthBar() 
            => _playerStatusBarView.HealthBar.UpdateValue(_playerSurvival.EntityHealth.CurrentHealth);

        private void UpdateShieldBar() 
            => _playerStatusBarView.ShieldBar.UpdateValue(_playerSurvival.EntityShield.CurrentShield);

        #endregion

    }
}