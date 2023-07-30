using SpaceRogue.Gameplay.GameEvent.Factories;
using SpaceRogue.Gameplay.Player;
using SpaceRogue.Scriptables.GameEvent;
using System;
using System.Collections.Generic;


namespace SpaceRogue.Gameplay.GameEvent
{
    public sealed class GameEventsController : IDisposable
    {

        #region Fields

        private readonly GeneralGameEventConfig _config;

        private readonly List<GameEvent> _gameEvents = new();

        #endregion


        #region CodeLife

        public GameEventsController(GeneralGameEventConfig generalGameEventConfig, GameEventFactory gameEventFactory, PlayerView playerView)
        {
            _config = generalGameEventConfig;

            foreach (var gameEvent in _config.GameEvents)
            {
                _gameEvents.Add(gameEventFactory.Create(gameEvent, playerView));
            }
        }

        public void Dispose()
        {
            foreach (var gameEvent in _gameEvents)
            {
                gameEvent.Dispose();
            }
            _gameEvents.Clear();
        }

        #endregion

    }
}