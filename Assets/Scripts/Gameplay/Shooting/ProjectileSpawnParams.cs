using SpaceRogue.Enums;
using SpaceRogue.Gameplay.Shooting.Scriptables;
using UnityEngine;


namespace SpaceRogue.Gameplay.Shooting
{
    public sealed class ProjectileSpawnParams
    {

        #region Properties

        public Vector2 Position { get; }
        public Quaternion Rotation { get; }
        public EntityType EntityType { get; }
        public ProjectileConfig Config { get; }

        #endregion


        #region CodeLife

        public ProjectileSpawnParams(Vector2 position, Quaternion rotation, EntityType entityType, ProjectileConfig config)
        {
            Position = position;
            Rotation = rotation;
            EntityType = entityType;
            Config = config;
        }

        public void Deconstruct(out Vector2 position, out Quaternion rotation, out ProjectileConfig config, out EntityType entityType)
        {
            position = Position;
            rotation = Rotation;
            config = Config;
            entityType = EntityType;
        }

        #endregion

    }
}