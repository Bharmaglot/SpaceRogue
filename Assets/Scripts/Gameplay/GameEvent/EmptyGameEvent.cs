using UnityEngine;


namespace SpaceRogue.Gameplay.GameEvent
{
    public sealed class EmptyGameEvent : GameEvent
    {
        protected override bool RunGameEvent()
        {
            Debug.Log($"EmptyGameEvent completed");
            return true;
        }
    }
}