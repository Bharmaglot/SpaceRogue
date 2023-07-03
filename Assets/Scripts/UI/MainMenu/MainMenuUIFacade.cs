using SpaceRogue.Services;
using UI.Game;
using Zenject;


namespace UI.MainMenu
{
    public sealed class MainMenuUIFacade : IInitializable
    {
        private readonly GameStateService _gameStateService;
        private readonly PlayerDataService _playerDataService;
        private readonly MainMenuCanvasView _mainMenuCanvasView;

        public MainMenuUIFacade(GameStateService gameStateService, PlayerDataService playerDataService, MainMenuCanvasView mainMenuCanvasView)
        {
            _gameStateService = gameStateService;
            _playerDataService = playerDataService;
            _mainMenuCanvasView = mainMenuCanvasView;
        }

        public void Initialize()
        {
            _mainMenuCanvasView.Init(_gameStateService.StartGame, _playerDataService.ResetRecord, _gameStateService.ExitGame, _playerDataService.RecordCompletedLevels);
        }
    }
}