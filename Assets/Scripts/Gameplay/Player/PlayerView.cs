using SpaceRogue.Abstraction;
using SpaceRogue.Enums;


namespace Gameplay.Player
{
    public sealed class PlayerView : EntityView
    {
        public override EntityType EntityType => EntityType.Player;
    }
}