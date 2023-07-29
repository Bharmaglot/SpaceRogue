using Gameplay.Events;
using SpaceRogue.Gameplay.GameProgress;
using SpaceRogue.Gameplay.Missions;
using System;


namespace SpaceRogue.UI.Game.LevelInfo
{
    public sealed class LevelInfoAdapter : IDisposable
    {

        #region Fields

        private readonly LevelInfoView _view;
        private readonly LevelProgress _levelProgress;

        private KillEnemiesMission _levelMission;

        #endregion


        #region CodeLife

        public LevelInfoAdapter(LevelInfoView levelInfoView, LevelProgress levelProgress)
        {
            _view = levelInfoView;
            _levelProgress = levelProgress;

            _levelProgress.LevelStarted += InitView;
        }

        public void Dispose()
        {
            _levelProgress.LevelStarted -= InitView;
            _levelMission.KillCountChanged -= UpdateDefeatedEnemiesCount;
        }

        #endregion


        #region Methods

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

        private void UpdateDefeatedEnemiesCount(int defeatedEnemiesCount) => _view.UpdateKillCounter(defeatedEnemiesCount.ToString());

        #endregion

    }
}