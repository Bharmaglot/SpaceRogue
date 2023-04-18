namespace Gameplay.GameProgress
{
    public sealed class CurrentGameProgress
    {
        private readonly LevelFactory _levelFactory;
        
        private Level _currentLevel;
        
        public int CurrentLevelNumber { get; private set; }

        public CurrentGameProgress(LevelFactory levelFactory)
        {
            _levelFactory = levelFactory;
            
            CurrentLevelNumber = 1;
            _currentLevel = levelFactory.Create(CurrentLevelNumber);
        }

        private void StartNextLevel()
        {
            _currentLevel.Dispose();
            CurrentLevelNumber += 1;
            _currentLevel = _levelFactory.Create(CurrentLevelNumber);
        }
    }
}