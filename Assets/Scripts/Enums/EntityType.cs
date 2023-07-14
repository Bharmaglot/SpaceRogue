using System;

namespace SpaceRogue.Enums
{
    [Flags]
    public enum EntityType
    {
        None = 0,
        Player = 2,
        Enemy = 4,
        EnemyAssistant = 6,
        Asteroid = 8
    }
}