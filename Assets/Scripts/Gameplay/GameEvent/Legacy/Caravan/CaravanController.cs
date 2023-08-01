using Gameplay.Enemy;
using Gameplay.Movement;
using SpaceRogue.Gameplay.Player;
using SpaceRogue.Scriptables.GameEvent;
using SpaceRogue.Abstraction;
using UnityEngine;
using Utilities.Reactive.SubscriptionProperty;
using Utilities.Unity;


namespace Gameplay.GameEvent.Caravan
{
    public sealed class CaravanController : BaseController
    {
        private readonly BaseCaravanGameEventConfig _baseCaravanGameEvent;
        private readonly PlayerView _playerView;
        private readonly CaravanView _caravanView;

        public SubscribedProperty<bool> OnDestroy = new();

        public CaravanController(GameEventConfig config, 
            CaravanView caravanView, Vector3 targetPosition)
        {
            var baseCaravanGameEvent = config as BaseCaravanGameEventConfig;
            _baseCaravanGameEvent = baseCaravanGameEvent
                ? baseCaravanGameEvent
                : throw new System.Exception("Wrong config type was provided");

            _caravanView = caravanView;
            AddGameObject(_caravanView.gameObject);

            AddCarnavalBehaviourController(_baseCaravanGameEvent.CaravanConfig.UnitMovement, targetPosition);

            AddEnemyGroup(_baseCaravanGameEvent, _caravanView.transform.position, _caravanView.transform);
        }

        private void AddCarnavalBehaviourController(UnitMovementConfig unitMovement, Vector3 targetPosition)
        {
            var behaviourController = new CaravanBehaviourController(new UnitMovementModel(unitMovement), _caravanView, targetPosition);
            AddController(behaviourController);
        }

        private void OnCaravanDestroyed()
        {
            if (_playerView == null)
            {
                OnDestroy.Value = true;
                return;
            }

            if (_caravanView.IsLastDamageFromPlayer)
            {
                StandardCaravanDestroyed();
                CaravanTrapDestroyed();
            }

            OnDestroy.Value = true;
        }

        private void StandardCaravanDestroyed()
        {
            var config = _baseCaravanGameEvent as CaravanGameEventConfig;
            
            if(config == null)
            {
                return;
            }
        }

        private void CaravanTrapDestroyed()
        {
            var config = _baseCaravanGameEvent as CaravanTrapGameEventConfig;

            if (config == null)
            {
                return;
            }

            Debug($"AlertRadius = {config.AlertRadius}");
        }

        private void AddEnemyGroup(BaseCaravanGameEventConfig config, Vector3 spawnPoint, Transform target)
        {
        }
    }
}