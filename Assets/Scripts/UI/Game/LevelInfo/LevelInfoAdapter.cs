using System;
using Gameplay.GameProgress;
using UnityEngine;

namespace UI.Game.LevelInfo
{
    public sealed class LevelInfoAdapter : IDisposable
    {
        private readonly LevelInfoView _view;
        private readonly CurrentLevelProgress _currentLevelProgress;

        public LevelInfoAdapter(LevelInfoView levelInfoView, CurrentLevelProgress currentLevelProgress)
        {
            _view = levelInfoView;
            _currentLevelProgress = currentLevelProgress;
            
            _view.Hide();
            _currentLevelProgress.LevelStarted += InitView;
            _currentLevelProgress.DefeatedEnemiesCountChange += UpdateDefeatedEnemiesCount;
        }

        public void Dispose()
        {
            _currentLevelProgress.LevelStarted -= InitView;
            _currentLevelProgress.DefeatedEnemiesCountChange -= UpdateDefeatedEnemiesCount;
        }

        private void InitView(Level level)
        {
            var enemiesToWinCount = Mathf.Clamp(level.EnemiesCountToWin, 1, level.EnemiesCreatedCount);
            _view.Init(level.CurrentLevelNumber, enemiesToWinCount);
            _view.Show();
        }

        private void UpdateDefeatedEnemiesCount(int defeatedEnemiesCount)
        {
            _view.UpdateKillCounter(defeatedEnemiesCount);
        }
    }
}