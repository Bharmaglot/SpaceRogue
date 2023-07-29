using UnityEngine;

namespace Gameplay.Events
{
    public sealed class PlayerSpawnedEventArgs
    {
        public Player.Player Player { get; }
        public Transform Transform { get; }

        public PlayerSpawnedEventArgs(Player.Player player, Transform transform)
        {
            Player = player;
            Transform = transform;
        }
    }
}