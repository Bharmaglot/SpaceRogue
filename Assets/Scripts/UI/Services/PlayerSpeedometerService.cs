using SpaceRogue.Gameplay.Player;
using SpaceRogue.Player.Movement;
using SpaceRogue.Services;
using SpaceRogue.UI.Game;
using System;
using UnityEngine;


namespace UI.Services
{
    public sealed class PlayerSpeedometerService : IDisposable
    {

        #region Fields

        private const string EXTRA_SPEED_COLOR = "#DBD080";

        private readonly PlayerSpeedometerView _playerSpeedometerView;
        private readonly Updater _updater;
        private readonly PlayerMovementFactory _movementFactory;

        private UnitMovement _playerMovement;

        #endregion


        #region CodeLife

        public PlayerSpeedometerService(Updater updater, PlayerInfoView playerInfoView, PlayerMovementFactory movementFactory)
        {
            _playerSpeedometerView = playerInfoView.PlayerSpeedometerView;
            _updater = updater;
            _movementFactory = movementFactory;

            _playerSpeedometerView.Hide();

            _movementFactory.PlayerMovementCreated += OnPlayerMovementCreated;
        }

        public void Dispose()
        {
            _movementFactory.PlayerMovementCreated -= OnPlayerMovementCreated;
            _updater.UnsubscribeFromUpdate(UpdateSpeedometer);
        }

        #endregion


        #region Methods

        private void OnPlayerMovementCreated(UnitMovement playerMovement)
        {
            _playerMovement = playerMovement;

            _playerSpeedometerView.Show();
            _playerSpeedometerView.Init(GetSpeedometerTextValue(
                _playerMovement.CurrentSpeed,
                _playerMovement.MaxSpeed,
                _playerMovement.ExtraSpeed));

            _updater.SubscribeToUpdate(UpdateSpeedometer);
        }

        private void UpdateSpeedometer()
            => _playerSpeedometerView.UpdateText(GetSpeedometerTextValue(
                _playerMovement.CurrentSpeed,
                _playerMovement.MaxSpeed,
                _playerMovement.ExtraSpeed));

        private string GetSpeedometerTextValue(float currentSpeed, float maximumSpeed, float extraSpeed)
        {
            var speed = (currentSpeed - extraSpeed) switch
            {
                < 0 => "R",
                _ => $"SPD: {Mathf.RoundToInt((currentSpeed - extraSpeed) / (maximumSpeed - extraSpeed) * 100.0f)}%"
            };

            if (!Mathf.Approximately(extraSpeed, 0.0f))
            {
                speed = $"{speed} <color={EXTRA_SPEED_COLOR}>+{extraSpeed}</color>";
            }
            return speed;
        }

        #endregion

    }
}