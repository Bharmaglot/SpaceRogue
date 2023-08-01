using Gameplay.Space.SpaceObjects;
using SpaceRogue.Gameplay.GameEvent;
using SpaceRogue.Gameplay.GameEvent.Comet;
using SpaceRogue.Gameplay.GameEvent.Factories;
using SpaceRogue.Gameplay.GameEvent.Scriptables;
using SpaceRogue.Gameplay.GameEvent.Supernova;
using SpaceRogue.Gameplay.Player;
using SpaceRogue.Gameplay.Pooling;
using SpaceRogue.Scriptables.GameEvent;
using UnityEngine;
using Zenject;


namespace SpaceRogue.Gameplay.Installers
{
    public sealed class GameEventsInstaller : MonoInstaller
    {

        #region Properties

        [field: SerializeField] public GameEventPool GameEventPool { get; private set; }

        #endregion


        #region Methods

        public override void InstallBindings()
        {
            InstallGameEventPool();
            InstallGameEvents();
            InstallGameEventIndicator();
            InstallCometGameEvent();
            InstallSupernovaGameEvent();
        }

        private void InstallGameEventPool()
        {
            Container
                .Bind<GameEventPool>()
                .FromInstance(GameEventPool)
                .AsSingle();
        }

        private void InstallGameEvents()
        {
            Container
                .BindIFactory<GameEventConfig, PlayerView, GameEvent.GameEvent>()
                .FromFactory<GameEventFactory>();

            Container
                .Bind<GameEventFactory>()
                .AsCached();

            Container
                .BindFactory<GeneralGameEventConfig, GameEventFactory, PlayerView, GameEventsController, GameEventsControllerFactory>()
                .AsSingle();
        }
        
        private void InstallGameEventIndicator()
        {
            Container
                .BindFactory<GameEventIndicatorView, GameEventIndicatorView, GameEventIndicatorViewFactory>()
                .AsSingle();
            
            Container
                .BindFactory<Collider2D, GameEventConfig, GameEventIndicator, GameEventIndicatorFactory>()
                .AsSingle();
        }

        private void InstallCometGameEvent()
        {
            Container
                .BindFactory<CometView, Vector2, float, CometView, CometViewFactory>()
                .AsSingle();
            
            Container
                .BindFactory<CometConfig, Vector2, Vector3, Comet, CometFactory>()
                .AsSingle();
        }
        
        private void InstallSupernovaGameEvent()
        {
            Container
                .BindFactory<SupernovaGameEventConfig, SpaceObjectView, Supernova, SupernovaFactory>()
                .AsSingle();
        }

        #endregion

    }
}