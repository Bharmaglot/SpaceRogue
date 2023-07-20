using SpaceRogue.Services;

namespace Gameplay.GameProgress
{
    public sealed class GameProgressState
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
            _currentLevel = levelFactory.Create(CurrentLevelNumber);
        }

        public void StartNextLevel()
        {
            _currentLevel.Dispose();
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