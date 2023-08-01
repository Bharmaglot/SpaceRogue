using SpaceRogue.Gameplay.Player;
using SpaceRogue.Services;
using Zenject;


namespace SpaceRogue.Gameplay.GameProgress
{
    public sealed class GameProgressState : IInitializable
    {

        #region Fields

        private readonly PlayerFactory _playerFactory;
        private readonly LevelFactory _levelFactory;
        private readonly GameStateService _gameStateService;

        private Player.Player _player;
        private Level _currentLevel;

        #endregion


        #region Properties

        public int CurrentLevelNumber { get; private set; }

        #endregion


        #region CodeLife

        public GameProgressState(PlayerFactory playerFactory, LevelFactory levelFactory, GameStateService gameStateService)
        {
            _playerFactory = playerFactory;
            _levelFactory = levelFactory;
            _gameStateService = gameStateService;

            CurrentLevelNumber = 1;
        }

        #endregion


        #region Methods

        public void Initialize()
        {
            _player = _playerFactory.Create();
            _currentLevel = _levelFactory.Create(_player, CurrentLevelNumber);
        }

        public void StartNextLevel()
        {
            _currentLevel.Dispose();
            _currentLevel = null;
            CurrentLevelNumber++;
            _currentLevel = _levelFactory.Create(_player, CurrentLevelNumber);
        }

        public void BackToMenu()
        {
            _currentLevel.Dispose();
            _gameStateService.GoToMenu();
        }

        #endregion

    }
}