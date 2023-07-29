namespace Gameplay.Events
{
    public class PlayerDestroyedEventArgs
    {
        public int CurrentLevel { get; }

        public PlayerDestroyedEventArgs(int currentLevel)
        {
            CurrentLevel = currentLevel;
        }
    }
}