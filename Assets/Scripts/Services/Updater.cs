using System;
using Gameplay.GameState;
using UnityEngine;
using Zenject;


namespace SpaceRogue.Services
{
    public sealed class Updater : ITickable, IFixedTickable, ILateTickable
    {
        private readonly GameStateService _gameStateService;
        
        private event Action OnUpdate = () => { };
        private event Action<float> OnDeltaTimeUpdate = _ => { };
        private event Action OnFixedUpdate = () => { };
        private event Action<float> OnDeltaTimeFixedUpdate = _ => { };
        private event Action OnLateUpdate = () => { };

        public Updater(GameStateService gameStateService)
        {
            _gameStateService = gameStateService;
        }

        public void SubscribeToUpdate(Action callback) => OnUpdate += callback;
        public void UnsubscribeFromUpdate(Action callback) => OnUpdate -= callback;
        public void SubscribeToUpdate(Action<float> callback) => OnDeltaTimeUpdate += callback;
        public void UnsubscribeFromUpdate(Action<float> callback) => OnDeltaTimeUpdate -= callback;

        public void SubscribeToFixedUpdate(Action callback) => OnFixedUpdate += callback;
        public void UnsubscribeFromFixedUpdate(Action callback) => OnFixedUpdate -= callback;
        public void SubscribeToFixedUpdate(Action<float> callback) => OnDeltaTimeFixedUpdate += callback;
        public void UnsubscribeFromFixedUpdate(Action<float> callback) => OnDeltaTimeFixedUpdate -= callback;

        public void SubscribeToLateUpdate(Action callback) => OnLateUpdate += callback;
        public void UnsubscribeFromLateUpdate(Action callback) => OnLateUpdate -= callback;

        public void Tick()
        {
            if (_gameStateService.CurrentState == GameState.GamePaused)
            {
                return;
            }
            
            OnUpdate.Invoke();
            OnDeltaTimeUpdate.Invoke(Time.deltaTime);
        }

        public void FixedTick()
        {
            if (_gameStateService.CurrentState == GameState.GamePaused)
            {
                return;
            }
            
            OnFixedUpdate.Invoke();
            OnDeltaTimeFixedUpdate.Invoke(Time.fixedDeltaTime);
        }

        public void LateTick()
        {
            if (_gameStateService.CurrentState == GameState.GamePaused)
            {
                return;
            }
            
            OnLateUpdate.Invoke();
        }
    }
}