using SpaceRogue.Abstraction;
using SpaceRogue.Enums;


namespace SpaceRogue.Services
{
    public sealed class GameStateService
    {

        #region Fields

        private readonly ISceneLoader _sceneLoader;

        #endregion


        #region Properties

        public GameState CurrentState { get; private set; }

        #endregion


        #region CodeLife

        public GameStateService(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
            CurrentState = GameState.Menu;
        }

        #endregion


        #region Methods

        public void StartGame()
        {
            if (CurrentState == GameState.Game)
            {
                return;
            }

            CurrentState = GameState.Game;
            _sceneLoader.LoadGameScene();
        }

        public void GoToMenu()
        {
            if (CurrentState == GameState.Menu)
            {
                return;
            }

            CurrentState = GameState.Menu;
            _sceneLoader.LoadMenuScene();
        }

        public void PauseGame()
        {
            if (CurrentState == GameState.GamePaused)
            {
                return;
            }

            CurrentState = GameState.GamePaused;
        }

        public void UnpauseGame()
        {
            if (CurrentState == GameState.Game)
            {
                return;
            }

            CurrentState = GameState.Game;
        }

        public void ExitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            UnityEngine.Application.Quit();
        }

        #endregion

    }
}