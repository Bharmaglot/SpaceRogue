using System;
using Gameplay.Events;
using Gameplay.GameProgress;
using Gameplay.Missions;
using Zenject;

namespace UI.Game.LevelInfo
{
    public sealed class LevelInfoAdapter : IInitializable, IDisposable
    {
        private readonly LevelInfoView _view;
        private readonly LevelProgress _levelProgress;

        private KillEnemiesMission _levelMission;

        public LevelInfoAdapter(LevelInfoView levelInfoView, LevelProgress levelProgress)
        {
            _view = levelInfoView;
            _levelProgress = levelProgress;
            
            _view.Hide();
        }
        
        public void Initialize()
        {
            _levelProgress.LevelStarted += InitView;
        }

        public void Dispose()
        {
            _levelProgress.LevelStarted -= InitView;
            _levelMission.KillCountChanged -= UpdateDefeatedEnemiesCount;
        }

        private void InitView(LevelStartedEventArgs level)
        {
            _levelMission = level.Mission;

            _levelMission.KillCountChanged += UpdateDefeatedEnemiesCount;
            
            _view.Init(
                level.Number.ToString(), 
                _levelMission.EnemiesKilled.ToString(), 
                _levelMission.EnemiesToKill.ToString());
            
            _view.Show();
        }

        private void UpdateDefeatedEnemiesCount(int defeatedEnemiesCount)
        {
            _view.UpdateKillCounter(defeatedEnemiesCount.ToString());
        }
    }
}