using Gameplay.Missions;

namespace Gameplay.Events
{
    public sealed class LevelStartedEventArgs
    {
        public int Number { get; }
        public KillEnemiesMission Mission { get; }
        public float MapCameraSize { get; }

        public LevelStartedEventArgs(int number, KillEnemiesMission mission, float mapCameraSize)
        {
            Number = number;
            Mission = mission;
            MapCameraSize = mapCameraSize;
        }
    }
}