namespace Gameplay.Events
{
    public class LevelCompletedEventArgs
    {
        public int Number { get; }

        public LevelCompletedEventArgs(int number)
        {
            Number = number;
        }
    }
}