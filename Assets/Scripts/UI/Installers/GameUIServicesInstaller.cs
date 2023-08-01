using Gameplay.Minimap;
using Scriptables;
using SpaceRogue.UI.Services;
using UI.Services;
using UnityEngine;
using Zenject;


namespace SpaceRogue.UI.Installers
{
    public sealed class GameUIServicesInstaller : MonoInstaller
    {

        #region Properties

        [field: SerializeField] public MinimapCamera MinimapCamera { get; private set; }
        [field: SerializeField] public MinimapConfig MinimapConfig { get; private set; }

        #endregion


        #region Methods

        public override void InstallBindings()
        {
            InstallObstacleUIEffectService();
            InstallPlayerInfoService();
            InstallPlayerStatusBarService();
            InstallPlayerSpeedometerService();
            InstallMinimapService();

            InstallEnemyStatusBarService();
            InstallGameEventIndicatorService();
        }

        private void InstallPlayerInfoService()
        {
            Container
                .BindInterfacesAndSelfTo<CharacterListService>()
                .AsSingle()
                .NonLazy();
            
            Container
                .BindInterfacesAndSelfTo<PlayerInfoService>()
                .AsSingle()
                .NonLazy();
        }

        private void InstallObstacleUIEffectService()
        {
            Container
                .BindInterfacesAndSelfTo<ObstacleUIEffectService>()
                .AsSingle()
                .NonLazy();
        }

        private void InstallPlayerStatusBarService()
        {
            Container
                .BindInterfacesAndSelfTo<PlayerStatusBarService>()
                .AsSingle()
                .NonLazy();
        }

        private void InstallPlayerSpeedometerService()
        {
            Container
                .BindInterfacesAndSelfTo<PlayerSpeedometerService>()
                .AsSingle()
                .NonLazy();
        }

        private void InstallMinimapService()
        {
            Container
                .Bind<MinimapCamera>()
                .FromInstance(MinimapCamera)
                .AsSingle()
                .NonLazy();

            Container
                .Bind<MinimapConfig>()
                .FromInstance(MinimapConfig)
                .AsSingle()
                .NonLazy();

            Container
                .BindInterfacesAndSelfTo<MinimapService>()
                .AsSingle()
                .NonLazy();
        }

        private void InstallEnemyStatusBarService()
        {
            Container
                .BindInterfacesAndSelfTo<EnemyStatusBarService>()
                .AsSingle()
                .NonLazy();
        }
        
        private void InstallGameEventIndicatorService()
        {
            Container
                .BindInterfacesAndSelfTo<GameEventIndicatorService>()
                .AsSingle()
                .NonLazy();
        }

        #endregion

    }
}