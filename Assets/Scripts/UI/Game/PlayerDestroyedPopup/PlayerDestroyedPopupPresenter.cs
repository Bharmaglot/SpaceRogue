using System;
using Gameplay.Events;
using Gameplay.GameProgress;

namespace UI.Game.PlayerDestroyedPopup
{
    public class PlayerDestroyedPopupPresenter : IDisposable
    {
        private readonly DestroyPlayerMessageView _view;
        private readonly GameProgressState _gameProgress;
        private readonly LevelProgress _levelProgress;

        public PlayerDestroyedPopupPresenter(
            LevelProgress levelProgress, 
            DestroyPlayerMessageView view,
            GameProgressState gameProgress
            )
        {
            _levelProgress = levelProgress;
            _view = view;
            _gameProgress = gameProgress;

            _levelProgress.PlayerDestroyed += OnPlayerDestroyed;
        }
        
        public void Dispose()
        {
            _levelProgress.PlayerDestroyed -= OnPlayerDestroyed;
        }

        private void OnPlayerDestroyed(PlayerDestroyedEventArgs args)
        {
            _view.Init((args.CurrentLevel -1).ToString(), OnBackToMenuButtonClicked);
            
            _view.Show();
        }

        private void OnBackToMenuButtonClicked()
        {
            _view.Hide();
            _gameProgress.BackToMenu();
        }
    }
}