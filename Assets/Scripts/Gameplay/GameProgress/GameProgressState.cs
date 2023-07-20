using SpaceRogue.Services;
using UnityEngine;
using Zenject;

namespace Gameplay.GameProgress
{
    public sealed class GameProgressState : IInitializable
    {
        private readonly LevelFactory _levelFactory;
        private readonly GameStateService _gameStateService;

        private Level _currentLevel;
        
        public int CurrentLevelNumber { get; private set; }

        public GameProgressState(LevelFactory levelFactory, GameStateService gameStateService)
        {
            _levelFactory = levelFactory;
            _gameStateService = gameStateService;

            CurrentLevelNumber = 1;
        }
        
        public void Initialize()
        {
            _currentLevel = _levelFactory.Create(CurrentLevelNumber);
        }

        public void StartNextLevel()
        {
            _currentLevel.Dispose();
            _currentLevel = null;
            CurrentLevelNumber++;
            _currentLevel = _levelFactory.Create(CurrentLevelNumber);
        }

        public void BackToMenu()
        {
            _currentLevel.Dispose();
            _gameStateService.GoToMenu();
        }
    }
}