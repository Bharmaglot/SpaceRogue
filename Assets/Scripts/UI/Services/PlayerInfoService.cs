using Gameplay.Events;
using Gameplay.Services;
using System;
using Gameplay.GameProgress;
using UI.Game;

namespace UI.Services
{
    public sealed class PlayerInfoService : IDisposable
    {
        private readonly LevelProgress _levelProgress;
        public PlayerWeaponView PlayerWeaponView { get; private set; }

        public PlayerInfoService(LevelProgress levelProgress, PlayerInfoView playerInfoView)
        {
            _levelProgress = levelProgress;
            PlayerWeaponView = playerInfoView.PlayerWeaponView;

            ShowPlayerInfo(false);

            _levelProgress.PlayerSpawned += OnPlayerSpawned;
        }

        public void Dispose()
        {
            _levelProgress.PlayerSpawned -= OnPlayerSpawned;
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