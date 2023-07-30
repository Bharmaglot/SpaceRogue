using SpaceRogue.Gameplay.Player;
using SpaceRogue.Scriptables.GameEvent;
using Zenject;


namespace SpaceRogue.Gameplay.GameEvent.Factories
{
    public sealed class GameEventsControllerFactory : PlaceholderFactory<GeneralGameEventConfig, GameEventFactory, PlayerView, GameEventsController>
    {
    }
}