using SpaceRogue.Abstraction;
using UnityEngine.SceneManagement;


namespace SpaceRogue.Services.SceneLoader
{
    public sealed class SceneLoader : ISceneLoader
    {
        private const string GameSceneName = "Game";
        private const string MenuSceneName = "MainMenu";
        
        public void LoadGameScene()
        {
            LoadScene(GameSceneName);
        }

        public void LoadMenuScene()
        {
            LoadScene(MenuSceneName);
        }

        private void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
    }
}