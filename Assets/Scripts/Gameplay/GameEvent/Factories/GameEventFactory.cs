using Gameplay.Mechanics.Timer;
using Gameplay.Player;
using SpaceRogue.Enums;
using SpaceRogue.Gameplay.GameEvent.Comet;
using SpaceRogue.Scriptables.GameEvent;
using System;
using Zenject;


namespace SpaceRogue.Gameplay.GameEvent.Factories
{
    public sealed class GameEventFactory : IFactory<GameEventConfig, PlayerView, GameEvent>
    {

        #region Events

        public event Action<GameEvent> GameEventCreated;

        #endregion


        #region Fields

        private readonly TimerFactory _timerFactory;
        private readonly CometFactory _cometFactory;

        #endregion


        #region CodeLife

        public GameEventFactory(TimerFactory timerFactory, CometFactory cometFactory)
        {
            _timerFactory = timerFactory;
            _cometFactory = cometFactory;
        }

        #endregion


        #region Methods

        public GameEvent Create(GameEventConfig gameEventConfig, PlayerView playerView)
        {
            GameEvent gameEvent = gameEventConfig.GameEventType switch
            {
                GameEventType.Empty => new EmptyGameEvent(),
                GameEventType.Comet => new CometGameEvent(gameEventConfig as CometGameEventConfig, _timerFactory, playerView, _cometFactory),
                GameEventType.Supernova => new EmptyGameEvent(),
                GameEventType.Caravan => new EmptyGameEvent(),
                GameEventType.CaravanTrap => new EmptyGameEvent(),
                _ => throw new ArgumentOutOfRangeException(nameof(gameEventConfig.GameEventType), gameEventConfig.GameEventType, $"A not-existent game event type is provided")
            };
            GameEventCreated?.Invoke(gameEvent);
            return gameEvent;
        }

        #endregion

    }
}