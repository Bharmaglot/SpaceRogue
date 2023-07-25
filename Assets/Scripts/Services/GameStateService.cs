using Gameplay.GameState;
using SpaceRogue.Abstraction;


namespace SpaceRogue.Services
{
    public sealed class GameStateService
    {
        private readonly ISceneLoader _sceneLoader;
        public GameState CurrentState { get; private set; }

        public GameStateService(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
            CurrentState = GameState.Menu;
        }

        public void StartGame()
        {
            if (CurrentState == GameState.Game) return;
            CurrentState = GameState.Game;
            _sceneLoader.LoadGameScene();
        }

        public void GoToMenu()
        {
            if (CurrentState == GameState.Menu) return;
            CurrentState = GameState.Menu;
            _sceneLoader.LoadMenuScene();
        }

        public void PauseGame()
        {
            if (CurrentState != GameState.Game) return;
            CurrentState = GameState.GamePaused;
        }

        public void UnpauseGame()
        {
            if (CurrentState != GameState.GamePaused) return;
            CurrentState = GameState.Game;
        }

        public void ExitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            UnityEngine.Application.Quit();
        }
    }
}