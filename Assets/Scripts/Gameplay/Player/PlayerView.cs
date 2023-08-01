using SpaceRogue.Abstraction;
using SpaceRogue.Enums;
using UnityEngine;


namespace SpaceRogue.Gameplay.Player
{
    [RequireComponent(typeof(SpriteRenderer))]
    public sealed class PlayerView : EntityViewBase
    {
        [field: SerializeField] public SpriteRenderer SpriteRenderer { get; private set; }

        public override EntityType EntityType => EntityType.Player;
    }
}