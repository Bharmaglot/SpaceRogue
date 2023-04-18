using System;
using Gameplay.GameProgress;
using UnityEngine;

namespace UI.Game.LevelInfo
{
    public sealed class LevelInfoAdapter : IDisposable
    {
        private readonly LevelInfoView _view;
        private readonly LevelProgress _levelProgress;

        public LevelInfoAdapter(LevelInfoView levelInfoView, LevelProgress levelProgress)
        {
            _view = levelInfoView;
            _levelProgress = levelProgress;
            
            _view.Hide();
            _levelProgress.LevelStarted += InitView;
            _levelProgress.DefeatedEnemiesCountChange += UpdateDefeatedEnemiesCount;
        }

        public void Dispose()
        {
            _levelProgress.LevelStarted -= InitView;
            _levelProgress.DefeatedEnemiesCountChange -= UpdateDefeatedEnemiesCount;
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