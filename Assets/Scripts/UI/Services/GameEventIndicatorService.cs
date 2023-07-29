using SpaceRogue.Gameplay.GameEvent;
using SpaceRogue.Gameplay.GameEvent.Factories;
using SpaceRogue.Scriptables.GameEvent;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace SpaceRogue.UI.Services
{
    public sealed class GameEventIndicatorService : IDisposable
    {

        #region Fields

        private readonly GameEventFactory _gameEventFactory;
        private readonly GameEventIndicatorFactory _gameEventIndicatorFactory;

        private readonly List<GameEvent> _gameEvents = new();

        #endregion


        #region CodeLife

        public GameEventIndicatorService(
            GameEventFactory gameEventFactory,
            GameEventIndicatorFactory gameEventIndicatorFactory)
        {
            _gameEventFactory = gameEventFactory;
            _gameEventIndicatorFactory = gameEventIndicatorFactory;

            _gameEventFactory.GameEventCreated += OnGameEventCreated;
        }

        public void Dispose()
        {
            _gameEventFactory.GameEventCreated -= OnGameEventCreated;
            foreach (var gameEvent in _gameEvents)
            {
                gameEvent.CreateIndicator -= CreateIndicator;
                gameEvent.GameEventDisposed -= OnGameEventDisposed;
            }
            _gameEvents.Clear();
        }

        #endregion


        #region Methods

        private void OnGameEventCreated(GameEvent gameEvent)
        {
            gameEvent.CreateIndicator += CreateIndicator;
            gameEvent.GameEventDisposed += OnGameEventDisposed;
            _gameEvents.Add(gameEvent);
        }

        private void CreateIndicator(Collider2D collider, GameEventConfig config) 
            => _gameEventIndicatorFactory.Create(collider, config);

        private void OnGameEventDisposed(GameEvent gameEvent)
        {
            gameEvent.CreateIndicator -= CreateIndicator;
            gameEvent.GameEventDisposed -= OnGameEventDisposed;
            _gameEvents.Remove(gameEvent);
        }

        #endregion

    }
}