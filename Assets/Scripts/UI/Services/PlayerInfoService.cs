using Gameplay.Events;
using Gameplay.Services;
using System;
using Gameplay.GameProgress;
using UI.Game;

namespace UI.Services
{
    public sealed class PlayerInfoService : IDisposable
    {
        private readonly CurrentLevelProgress _currentLevelProgress;
        public PlayerWeaponView PlayerWeaponView { get; private set; }

        public PlayerInfoService(CurrentLevelProgress currentLevelProgress, PlayerInfoView playerInfoView)
        {
            _currentLevelProgress = currentLevelProgress;
            PlayerWeaponView = playerInfoView.PlayerWeaponView;

            ShowPlayerInfo(false);

            _currentLevelProgress.PlayerSpawned += OnPlayerSpawned;
        }

        public void Dispose()
        {
            _currentLevelProgress.PlayerSpawned -= OnPlayerSpawned;
        }

        private void OnPlayerSpawned(PlayerSpawnedEventArgs obj)
        {
            ShowPlayerInfo(true);
        }

        private void ShowPlayerInfo(bool enable)
        {
            PlayerWeaponView.gameObject.SetActive(enable);
        }
    }
}