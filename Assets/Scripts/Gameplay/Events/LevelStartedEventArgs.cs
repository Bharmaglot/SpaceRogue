using SpaceRogue.Gameplay.Missions;


namespace Gameplay.Events
{
    public sealed class LevelStartedEventArgs
    {

        #region Properties

        public int Number { get; }
        public KillEnemiesMission Mission { get; }
        public float MapCameraSize { get; }

        #endregion


        #region CodeLife

        public LevelStartedEventArgs(int number, KillEnemiesMission mission, float mapCameraSize)
        {
            Number = number;
            Mission = mission;
            MapCameraSize = mapCameraSize;
        }

        #endregion

    }
}