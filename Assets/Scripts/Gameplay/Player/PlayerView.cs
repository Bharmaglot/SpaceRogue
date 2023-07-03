using SpaceRogue.Abstraction;
using SpaceRogue.Enums;


namespace Gameplay.Player
{
    public sealed class PlayerView : EntityViewBase
    {
        public override EntityType EntityType => EntityType.Player;
    }
}