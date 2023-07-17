using Gameplay.Player;
using Scriptables.GameEvent;
using System;
using Gameplay.GameEvent.Caravan;
using Gameplay.GameEvent.Comet;
using Gameplay.GameEvent.Supernova;
using Utilities.ResourceManagement;
using SpaceRogue.Abstraction;


namespace Gameplay.GameEvent
{
    public sealed class GeneralGameEventsController : BaseController
    {
        private readonly ResourcePath _configPath = new(Constants.Configs.GameEvent.GeneralGameEventConfig);
        private readonly GeneralGameEventConfig _config;        

        public GeneralGameEventsController(PlayerController playerController)
        {
            _config = ResourceLoader.LoadObject<GeneralGameEventConfig>(_configPath);

            foreach (var gameEvent in _config.GameEvents)
            {
                InitializeGameEvent(gameEvent, playerController);
            }
        }

        private void InitializeGameEvent(GameEventConfig gameEvent, PlayerController playerController)
        {
            var gameEventController = CreateGameEvent(gameEvent, playerController);
            AddController(gameEventController);
        }

        private GameEventController CreateGameEvent(GameEventConfig gameEvent, PlayerController playerController)
        {
            return gameEvent.GameEventType switch
            {
                GameEventType.Empty => new EmptyGameEventController(gameEvent, playerController),
                GameEventType.Comet => new CometGameEventController(gameEvent, playerController),
                GameEventType.Supernova => new SupernovaGameEventController(gameEvent, playerController),
                GameEventType.Caravan => new CaravanGameEventController(gameEvent, playerController),
                GameEventType.CaravanTrap => new CaravanTrapGameEventController(gameEvent, playerController),
                _ => throw new ArgumentOutOfRangeException(nameof(gameEvent.GameEventType), gameEvent.GameEventType, "A not-existent game event type is provided")
            };
        }
    }
}