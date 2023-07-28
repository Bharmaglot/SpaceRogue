using System;


namespace SpaceRogue.Gameplay.Missions
{
    public abstract class BaseMission
    {
        public abstract event Action Completed;
    }
}