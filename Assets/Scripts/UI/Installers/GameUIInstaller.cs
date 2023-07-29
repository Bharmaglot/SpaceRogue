using Gameplay.Survival;
using SpaceRogue.UI.Game;
using UI.Game;
using UnityEngine;
using Zenject;
using SpaceRogue.UI.Game.LevelFinishedPopup;
using SpaceRogue.UI.Game.LevelInfo;
using SpaceRogue.UI.Game.PlayerDestroyedPopup;
using SpaceRogue.Gameplay.GameEvent;

namespace SpaceRogue.UI.Installers
{
    public sealed class GameUIInstaller : MonoInstaller
    {

        #region Properties

        [field: Header("GameUICanvas")]
        [field: SerializeField] public MainCanvas MainCanvas { get; private set; }
        [field: SerializeField] public GameCanvasView GameCanvasView { get; private set; }

        [field: Header("Permanent UI")]
        [field: SerializeField] public ObstacleUIEffectView ObstacleUIEffectView { get; private set; }
        [field: SerializeField] public PlayerInfoView PlayerInfoView { get; private set; }
        [field: SerializeField] public LevelInfoView LevelInfoView { get; private set; }
        [field: SerializeField] public MinimapView MinimapView { get; private set; }

        [field: Header("For instantiate other UI")]
        [field: SerializeField] public EnemyHealthBarsView EnemyHealthBarsView { get; private set; }
        [field: SerializeField] public HealthShieldStatusBarView HealthShieldStatusBarView { get; private set; }
        [field: SerializeField] public HealthStatusBarView HealthStatusBarView { get; private set; }
        [field: SerializeField] public IndicatorsView IndicatorsView { get; private set; }
        [field: SerializeField] public GameEventIndicatorView GameEventIndicatorView { get; private set; }

        [field: Header("Popups")]
        [field: SerializeField] public DestroyPlayerMessageView PlayerDestroyedPopup { get; private set; }
        [field: SerializeField] public NextLevelMessageView LevelCompletedPopup { get; private set; }

        #endregion


        #region Methods

        public override void InstallBindings()
        {
            BindGameUICanvas();
            BindObstacleUIEffect();
            BindPlayerInfo();
            BindLevelInfo();
            BindMinimap();
            BindFloatStatusBarFactory();
            BindEnemyStatusBars();
            BindGameEventIndicators();
            BindPopups();
        }
        
        private void BindPopups()
        {
            BindPlayerDestroyedPopup();
            BindLevelCompletedPopup();
        }

        private void BindGameUICanvas()
        {
            Container
                .Bind<MainCanvas>()
                .FromInstance(MainCanvas)
                .AsSingle()
                .NonLazy();

            Container
                .Bind<GameCanvasView>()
                .FromInstance(GameCanvasView)
                .AsSingle()
                .NonLazy();
        }

        private void BindObstacleUIEffect()
        {
            Container
                .Bind<ObstacleUIEffectView>()
                .FromInstance(ObstacleUIEffectView)
                .AsSingle()
                .NonLazy();
        }

        private void BindPlayerInfo()
        {
            Container
                .Bind<PlayerInfoView>()
                .FromInstance(PlayerInfoView)
                .AsSingle()
                .NonLazy();
        }

        private void BindLevelInfo()
        {
            Container
                .Bind<LevelInfoView>()
                .FromInstance(LevelInfoView)
                .AsSingle()
                .NonLazy();

            Container
                .Bind<LevelInfoAdapter>()
                .AsSingle()
                .NonLazy();
        }

        private void BindMinimap()
        {
            Container
                .Bind<MinimapView>()
                .FromInstance(MinimapView)
                .AsSingle()
                .NonLazy();
        }

        private void BindFloatStatusBarFactory()
        {
            Container
                .BindFactory<HealthStatusBarView, Collider2D, EntitySurvival, FloatStatusBar, FloatStatusBarFactory>()
                .AsSingle();
        }

        private void BindEnemyStatusBars()
        {
            Container
                .Bind<EnemyHealthBarsView>()
                .FromInstance(EnemyHealthBarsView)
                .AsSingle()
                .NonLazy();

            Container
                .Bind<HealthShieldStatusBarView>()
                .FromInstance(HealthShieldStatusBarView)
                .WhenInjectedInto<EnemyStatusBarViewFactory>();

            Container
                .Bind<HealthStatusBarView>()
                .FromInstance(HealthStatusBarView)
                .WhenInjectedInto<EnemyStatusBarViewFactory>();

            Container
                .BindFactory<EntitySurvival, EnemyHealthBarsView, HealthStatusBarView, EnemyStatusBarViewFactory>()
                .AsSingle();
        }

        private void BindGameEventIndicators()
        {
            Container
                .Bind<IndicatorsView>()
                .FromInstance(IndicatorsView)
                .AsSingle()
                .NonLazy();
            
            Container
                .Bind<GameEventIndicatorView>()
                .FromInstance(GameEventIndicatorView)
                .AsSingle()
                .NonLazy();
        }

        private void BindPlayerDestroyedPopup()
        {
            Container
                .Bind<DestroyPlayerMessageView>()
                .FromInstance(PlayerDestroyedPopup)
                .AsSingle()
                .NonLazy();

            Container
                .Bind<PlayerDestroyedPopupPresenter>()
                .AsSingle()
                .NonLazy();
        }

        private void BindLevelCompletedPopup()
        {
            Container
                .Bind<NextLevelMessageView>()
                .FromInstance(LevelCompletedPopup)
                .AsSingle()
                .NonLazy();

            Container
                .Bind<LevelFinishedPopupPresenter>()
                .AsSingle()
                .NonLazy();
        }

        #endregion

    }
}