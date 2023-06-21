using SpaceRogue.Abstraction;
using SpaceRogue.Services;
using SpaceRogue.Services.SceneLoader;
using Zenject;


namespace Installers
{
    public sealed class GlobalsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindSceneLoader();
            BindGameState();
            BindPlayerData();
            BindUpdater();
        }

        private void BindSceneLoader()
        {
            Container
                .Bind<ISceneLoader>()
                .To<SceneLoader>()
                .AsSingle()
                .NonLazy();
        }

        private void BindGameState()
        {
            Container
                .Bind<GameStateService>()
                .AsSingle()
                .NonLazy();
        }

        private void BindPlayerData()
        {
            Container
                .BindInterfacesAndSelfTo<PlayerDataService>()
                .AsSingle()
                .NonLazy();
        }

        private void BindUpdater()
        {
            Container
                .BindInterfacesAndSelfTo<Updater>()
                .AsSingle()
                .NonLazy();
        }
    }
}