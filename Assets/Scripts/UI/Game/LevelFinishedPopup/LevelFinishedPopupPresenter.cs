using System;
using Gameplay.Events;
using Gameplay.GameProgress;

namespace UI.Game.LevelFinishedPopup
{
    public class LevelFinishedPopupPresenter : IDisposable
    {
        private readonly LevelProgress _levelProgress;
        private readonly NextLevelMessageView _view;
        private readonly GameProgressState _gameProgress;

        public LevelFinishedPopupPresenter(
            LevelProgress levelProgress,
            NextLevelMessageView view, 
            GameProgressState gameProgress
            )
        {
            _levelProgress = levelProgress;
            _view = view;
            _gameProgress = gameProgress;

            _levelProgress.LevelCompleted += OnLevelCompleted;
        }

        public void Dispose()
        {
            _levelProgress.LevelCompleted -= OnLevelCompleted;
        }

        private void OnLevelCompleted(LevelCompletedEventArgs args)
        {
            _view.Init(args.Number.ToString(), OnNextLevelButtonClicked);
            _view.Show();
        }

        private void OnNextLevelButtonClicked()
        {
            _view.Hide();
            _gameProgress.StartNextLevel();
        }
    }
}