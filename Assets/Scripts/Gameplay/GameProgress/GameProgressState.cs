namespace Gameplay.GameProgress
{
    public sealed class GameProgressState
    {
        private readonly LevelFactory _levelFactory;

        private Level _currentLevel;
        
        public int CurrentLevelNumber { get; private set; }

        public GameProgressState(LevelFactory levelFactory)
        {
            _levelFactory = levelFactory;

            CurrentLevelNumber = 1;
            _currentLevel = levelFactory.Create(CurrentLevelNumber);
        }

        public void StartNextLevel()
        {
            _currentLevel.Dispose();
            CurrentLevelNumber += 1;
            _currentLevel = _levelFactory.Create(CurrentLevelNumber);
        }
    }
}