using Gameplay.Player;
using SpaceRogue.Scriptables.GameEvent;
using UnityEngine;
using Utilities.Mathematics;
using Utilities.Unity;

namespace Gameplay.GameEvent.Caravan
{
    public class CaravanGameEventController : LegacyGameEventController
    {
        private const int MaxCountOfCaravanSpawnTries = 10;

        private readonly BaseCaravanGameEventConfig _baseCaravanGameEvent;
        private readonly PlayerView _playerView;
        private readonly float _orthographicSize;
        private readonly float _caravanSize;

        private CaravanController _caravanController;
        private bool _isStopped;

        public CaravanGameEventController(GameEventConfig config, PlayerController playerController) : base(config, playerController)
        {
            var baseCaravanGameEvent = config as BaseCaravanGameEventConfig;
            _baseCaravanGameEvent = baseCaravanGameEvent
                ? baseCaravanGameEvent
                : throw new System.Exception("Wrong config type was provided");

            _playerView = _playerController.View;
            _orthographicSize = UnityEngine.Camera.main.orthographicSize;
            _caravanSize = _baseCaravanGameEvent.CaravanConfig.CaravanView.transform.localScale.MaxVector3CoordinateOnPlane();
        }

        protected override bool RunGameEvent()
        {
            if (_isStopped)
            {
                return true;
            }

            if (!TryGetNewCaravanPositionAndTargetPosition(out var position, out var targetPosition))
            {
                Debug("No place for Caravan");
                return false;
            }

            var caravanView = CreateCaravanView(position);
            _caravanController = new(_config, _playerController, caravanView, targetPosition);
            _caravanController.OnDestroy.Subscribe(DestroyController);
            AddController(_caravanController);
            AddGameEventObjectToUIController(caravanView.gameObject);
            return true;
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            _caravanController?.OnDestroy.Unsubscribe(DestroyController);
        }

        protected override void OnPlayerDestroyed()
        {
            _isStopped = true;
        }

        private void DestroyController(bool onDestroy)
        {
            if (onDestroy)
            {
                Dispose();
            }
        }

        private bool TryGetNewCaravanPositionAndTargetPosition(out Vector3 position, out Vector3 targetPosition)
        {
            var tryCount = 0;
            var radius = _caravanSize + _baseCaravanGameEvent.EnemyCount * 2;
            do
            {
                position = GetRandomCaravanPosition();
                targetPosition = GetRandomCaravanTargetPosition(position);
                tryCount++;
            }
            while ((UnityHelper.IsAnyObjectAtPosition(position, radius)
                   || UnityHelper.IsAnyObjectAtPosition(targetPosition, radius))
                   && tryCount <= MaxCountOfCaravanSpawnTries);

            if (tryCount > MaxCountOfCaravanSpawnTries)
            {
                return false;
            }

            return true;
        }

        private Vector3 GetRandomCaravanPosition()
        {
            var angleDirection = RandomPicker.PickRandomAngle(360).normalized;
            var playerPosition = _playerView.transform.position;
            var offset = _orthographicSize * 2 + _caravanSize + _baseCaravanGameEvent.SpawnOffset;
            var position = playerPosition + angleDirection * offset;
            return position;
        }

        private Vector3 GetRandomCaravanTargetPosition(Vector3 caravanPosition)
        {
            var angleDirection = RandomPicker.PickRandomAngle(360).normalized;
            var offset = _caravanSize + _baseCaravanGameEvent.PathDistance;
            var position = caravanPosition + angleDirection * offset;
            return position;
        }

        private CaravanView CreateCaravanView(Vector3 position)
        {
            var caravanView = Object.Instantiate(_baseCaravanGameEvent.CaravanConfig.CaravanView, position, Quaternion.identity);
            return caravanView;
        }
    }
}