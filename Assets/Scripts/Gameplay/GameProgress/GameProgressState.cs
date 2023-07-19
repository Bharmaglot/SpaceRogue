using System;

namespace Gameplay.GameProgress
{
    public sealed class GameProgressState : IDisposable
    {
        private readonly LevelFactory _levelFactory;
        private readonly LevelProgress _levelProgress;

        private Level _currentLevel;
        
        public int CurrentLevelNumber { get; private set; }

        public GameProgressState(LevelFactory levelFactory, LevelProgress levelProgress)
        {
            _levelFactory = levelFactory;
            _levelProgress = levelProgress;

            CurrentLevelNumber = 1;
            _currentLevel = levelFactory.Create(CurrentLevelNumber);

            _levelProgress.LevelCompleted += StartNextLevel;
        }
        
        public void Dispose()
        {
            _levelProgress.LevelCompleted -= StartNextLevel;
        }

        private void StartNextLevel()
        {
            _currentLevel.Dispose();
            CurrentLevelNumber += 1;
            _currentLevel = _levelFactory.Create(CurrentLevelNumber);
        }
    }
}