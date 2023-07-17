using System;

namespace Gameplay.Missions
{
    public abstract class BaseMission
    {
        public abstract event Action Completed;
    }
}