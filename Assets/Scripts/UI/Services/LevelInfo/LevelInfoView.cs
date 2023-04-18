using UI.Abstracts;
using UI.Game;

namespace UI.Services.LevelInfo
{
    public sealed class LevelInfoView : IShowableView, IHideableView
    {
        private readonly LevelNumberView _levelNumberView;
        private readonly EnemiesCountView _enemiesCountView;

        public LevelInfoView(LevelNumberView levelNumberView, EnemiesCountView enemiesCountView)
        {
            _levelNumberView = levelNumberView;
            _enemiesCountView = enemiesCountView;
        }

        public void Init(int currentLevelNumber, int enemiesToWin)
        {
            
        }

        public void Show()
        {
            _levelNumberView.Show();
            _enemiesCountView.Show();
        }

        public void Hide()
        {
            _levelNumberView.Hide();
            _enemiesCountView.Hide();
        }

        public void UpdateKillCounter(int defeatedEnemiesCount)
        {
        }
    }
}