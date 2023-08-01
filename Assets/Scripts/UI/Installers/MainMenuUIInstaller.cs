using Scriptables;
using SpaceRogue.Gameplay.Background;
using UI.MainMenu;
using UnityEngine;
using Zenject;

namespace UI.Installers
{
    public sealed class MainMenuUIInstaller : MonoInstaller
    {
        [field: SerializeField] public Camera MainCamera { get; private set; }
        [field: SerializeField] public MainMenuCanvasView MainMenuCanvas { get; private set; }
        [field: SerializeField] public MenuBackgroundConfig MenuBackgroundConfig { get; private set; }
        [field: SerializeField] public MenuBackgroundView MenuBackground { get; private set; }

        public override void InstallBindings()
        {
            BindMainMenuCanvas();
            BindMainMenu();
            BindCamera();
            BindMainMenuBackground();
        }

        private void BindMainMenuCanvas()
        {
            Container
                .Bind<MainMenuCanvasView>()
                .FromInstance(MainMenuCanvas)
                .AsSingle()
                .NonLazy();
        }

        private void BindMainMenu()
        {
            Container
                .BindInterfacesAndSelfTo<MainMenuUIFacade>()
                .AsSingle()
                .NonLazy();
        }

        private void BindCamera()
        {
            Container
                .Bind<Camera>()
                .FromInstance(MainCamera)
                .AsSingle()
                .NonLazy();
        }

        private void BindMainMenuBackground()
        {
            Container
                .Bind<MenuBackgroundConfig>()
                .FromInstance(MenuBackgroundConfig)
                .AsSingle()
                .NonLazy();

            Container
                .Bind<MenuBackgroundView>()
                .FromInstance(MenuBackground)
                .AsSingle()
                .NonLazy();

            Container
                .BindInterfacesAndSelfTo<MainMenuBackground>()
                .AsSingle()
                .NonLazy();
        }
    }
}