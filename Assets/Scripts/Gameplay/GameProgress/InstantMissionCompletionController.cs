using System;
using Gameplay.Events;
using Gameplay.Missions;
using SpaceRogue.InputSystem;
using Zenject;

namespace Gameplay.GameProgress
{
    public sealed class InstantMissionCompletionController : IDisposable
    {
        private readonly PlayerInput _playerInput;
        private readonly LevelProgress _levelProgress;

        private KillEnemiesMission _levelMission;

        public InstantMissionCompletionController(PlayerInput playerInput, LevelProgress levelProgress)
        {
            _playerInput = playerInput;
            _levelProgress = levelProgress;
            
            _levelProgress.LevelStarted += OnLevelCreated;
            _levelProgress.LevelFinished += OnLevelFinished;
            _playerInput.NextLevelInput += OnNextLevelPressed;
        }

        public void Dispose()
        {
            _levelProgress.LevelStarted -= OnLevelCreated;
            _levelProgress.LevelFinished -= OnLevelFinished;
            _playerInput.NextLevelInput -= OnNextLevelPressed;
        }
        
        private void OnLevelCreated(LevelStartedEventArgs level)
        {
            _levelMission = level.Mission;
        }
        
        private void OnLevelFinished()
        {
            _levelMission = null;
        }

        private void OnNextLevelPressed(bool isPressed)
        {
            if (!isPressed)
                return;

            _levelMission?.CompleteInstantly();
        }
    }
}