using UnityEngine;


namespace SpaceRogue.Gameplay.Events
{
    public sealed class PlayerSpawnedEventArgs
    {

        #region Properties

        public Player.Player Player { get; }
        public Transform Transform { get; }

        #endregion


        #region CodeLife

        public PlayerSpawnedEventArgs(Player.Player player, Transform transform)
        {
            Player = player;
            Transform = transform;
        }

        #endregion

    }
}