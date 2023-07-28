using Gameplay.Events;
using SpaceRogue.Services;
using System;
using UnityEngine;


namespace SpaceRogue.Gameplay.GameProgress
{
    public class LevelCompleteController : IDisposable
    {

        #region Fields

        private readonly LevelProgress _levelProgress;
        private readonly GameStateService _gameStateService;

        #endregion


        #region CodeLife

        public LevelCompleteController(LevelProgress levelProgress, GameStateService gameStateService)
        {
            _levelProgress = levelProgress;
            _gameStateService = gameStateService;

            _levelProgress.LevelStarted += OnLevelStarted;
            _levelProgress.LevelFinished += OnLevelFinished;
        }

        #endregion


        #region Methods

        public void Dispose()
        {
            _levelProgress.LevelStarted -= OnLevelStarted;
            _levelProgress.LevelFinished -= OnLevelFinished;
        }

        private void OnLevelStarted(LevelStartedEventArgs _)
        {
            Time.timeScale = 1;
            _gameStateService.UnpauseGame();
        }

        private void OnLevelFinished()
        {
            Time.timeScale = 0;
            _gameStateService.PauseGame();
        }

        #endregion

    }
}