using System;
using Gameplay.Events;
using SpaceRogue.Services;
using UnityEngine;

namespace Gameplay.GameProgress
{
    public class LevelCompleteController : IDisposable
    {
        private readonly LevelProgress _levelProgress;
        private readonly GameStateService _gameStateService;

        public LevelCompleteController(LevelProgress levelProgress, GameStateService gameStateService)
        {
            _levelProgress = levelProgress;
            _gameStateService = gameStateService;

            _levelProgress.LevelStarted += OnLevelStarted;
            _levelProgress.LevelFinished += OnLevelFinished;
        }

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
    }
}