using Gameplay.Events;
using SpaceRogue.Gameplay.GameProgress;
using System;


namespace SpaceRogue.UI.Game.LevelFinishedPopup
{
    public class LevelFinishedPopupPresenter : IDisposable
    {

        #region Fields

        private readonly LevelProgress _levelProgress;
        private readonly NextLevelMessageView _view;
        private readonly GameProgressState _gameProgress;

        #endregion


        #region CodeLife

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

        public void Dispose() => _levelProgress.LevelCompleted -= OnLevelCompleted;

        #endregion


        #region Methods

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

        #endregion

    }
}