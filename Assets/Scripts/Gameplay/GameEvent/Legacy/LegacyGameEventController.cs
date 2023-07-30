using Gameplay.Mechanics.Timer;
using Gameplay.Player;
using SpaceRogue.Scriptables.GameEvent;
using SpaceRogue.Abstraction;
using SpaceRogue.Services;
using SpaceRogue.Services.SceneLoader;
using UI.Game;
using UnityEngine;
using Utilities.Mathematics;
using Utilities.ResourceManagement;
using Random = System.Random;


namespace Gameplay.GameEvent
{
    public abstract class LegacyGameEventController : BaseController
    {
        protected readonly GameEventConfig _config;
        //protected readonly PlayerController _playerController;
        protected Timer _timer;

        private readonly ResourcePath _gameEventIndicatorCanvasPath =
            new(Constants.Prefabs.Canvas.Game.GameEventIndicatorCanvas);

        private bool _isOnceSuccessfully;

        public LegacyGameEventController(GameEventConfig config/*, PlayerController playerController*/)
        {
            _config = config;
            //_playerController = playerController;
            //_playerController.PlayerDestroyed += OnPlayerDestroyed;
            //_playerController.OnControllerDispose += OnPlayerDestroyed;
            _timer = new(_config.ResponseTime, new Updater(new GameStateService(new SceneLoader())));
            _timer.Start();

            EntryPoint.SubscribeToUpdate(CheckEvent);
        }

        protected override void OnDispose()
        {
            _timer.Dispose();
            //_playerController.PlayerDestroyed -= OnPlayerDestroyed;
            //_playerController.OnControllerDispose -= OnPlayerDestroyed;
            EntryPoint.UnsubscribeFromUpdate(CheckEvent);
        }

        private void CheckEvent()
        {
            if (_timer.IsExpired)
            {
                if(RandomPicker.TakeChance(_config.Chance))
                {
                    _isOnceSuccessfully = RunGameEvent();
                }

                if (_config.IsRecurring || !_isOnceSuccessfully)
                {
                    _timer.Start();
                    return;
                }

                if (!_config.IsRecurring && _isOnceSuccessfully)
                {
                    EntryPoint.UnsubscribeFromUpdate(CheckEvent);
                }
            }
        }

        protected abstract bool RunGameEvent();

        protected virtual void OnPlayerDestroyed()
        {
            Dispose();
        }

        protected void AddGameEventObjectToUIController(GameObject gameObject, bool showUntilItIsVisibleOnce = false)
        {
            /*if (gameObject.TryGetComponent(out Collider2D collider))
            {
                var gameEventUIController = new GameEventUIController(
                    AddGameEventIndicatorView(GameUIController.GameEventIndicators), 
                    collider,
                    _config.Icon,
                    _config.IndicatorDiameter,
                    showUntilItIsVisibleOnce);
                AddController(gameEventUIController);
            }*/
        }

        //private GameEventIndicatorView AddGameEventIndicatorView(Transform transform)
        //{
        //    var gameEventIndicatorView = ResourceLoader.LoadPrefabAsChild<GameEventIndicatorView>
        //        (_gameEventIndicatorCanvasPath, transform);
        //    return gameEventIndicatorView;
        //}
    }
}