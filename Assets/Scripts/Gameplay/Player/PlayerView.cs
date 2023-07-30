using SpaceRogue.Abstraction;
using SpaceRogue.Enums;


namespace SpaceRogue.Gameplay.Player
{
    public sealed class PlayerView : EntityViewBase
    {
        public override EntityType EntityType => EntityType.Player;
    }
}