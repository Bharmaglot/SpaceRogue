using System;
using Gameplay.Events;
using Gameplay.GameProgress;
using SpaceRogue.Services;

namespace UI.Game.PlayerDestroyedPopup
{
    public class PlayerDestroyedPopupPresenter : IDisposable
    {
        private readonly DestroyPlayerMessageView _view;
        private readonly GameStateService _gameStateService;
        private readonly LevelProgress _levelProgress;

        public PlayerDestroyedPopupPresenter(
            LevelProgress levelProgress, 
            DestroyPlayerMessageView view,
            GameStateService gameStateService
            )
        {
            _levelProgress = levelProgress;
            _view = view;
            _gameStateService = gameStateService;

            _levelProgress.PlayerDestroyed += OnPlayerDestroyed;
        }
        
        public void Dispose()
        {
            _levelProgress.PlayerDestroyed -= OnPlayerDestroyed;
        }

        private void OnPlayerDestroyed(PlayerDestroyedEventArgs args)
        {
            _view.Init(args.CurrentLevel.ToString(), OnBackToMenuButtonClicked);
            
            _view.Show();
        }

        private void OnBackToMenuButtonClicked()
        {
            _view.Hide();
            _gameStateService.GoToMenu();
        }
    }
}