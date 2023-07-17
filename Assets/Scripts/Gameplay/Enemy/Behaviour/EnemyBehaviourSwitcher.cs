using Gameplay.Movement;
using SpaceRogue.Enemy.Movement;
using System;
using UnityEngine;


namespace Gameplay.Enemy.Behaviour
{
    public sealed class EnemyBehaviourSwitcher : IDisposable
    {
        private readonly EnemyView _view;
        private readonly EnemyInput _enemyInput;
        private readonly UnitMovementModel _unitMovementModel;
        private readonly EnemyBehaviourConfig _enemyConfig;
        private readonly EnemyBehaviourFactory _behaviourFactory;
        private readonly Transform _escortTarget;

        private event Action<EnemyState> _enemyStateChanged = _ => { };
        
        public EnemyBehaviour CurrentBehaviour { get; private set; }

        public EnemyBehaviourSwitcher(
            EnemyView view,
            EnemyInput enemyInput,
            UnitMovementModel unitMovementModel,
            EnemyBehaviourConfig enemyConfig,
            EnemyBehaviourFactory behaviourFactory,
            Transform escortTarget = null)
        {
            _view = view;
            _enemyInput = enemyInput;
            _unitMovementModel = unitMovementModel;
            _enemyConfig = enemyConfig;
            _behaviourFactory = behaviourFactory;
            _escortTarget = escortTarget;

            _enemyStateChanged += OnEnemyStateChange;
            _enemyStateChanged(_enemyConfig.StartEnemyState);
        }

        public void Dispose()
        {
            _enemyStateChanged -= OnEnemyStateChange;
            CurrentBehaviour.Dispose();
        }

        private void OnEnemyStateChange(EnemyState newState)
        {
            var lastEnemyState = CurrentBehaviour == null ? EnemyState.PassiveRoaming : CurrentBehaviour.CurrentState;

            Transform targetTransform;
            if (newState == EnemyState.Escort)
            {
                targetTransform = _escortTarget;
            }
            else
            {
                targetTransform = CurrentBehaviour == null ? default : CurrentBehaviour.TargetTransform;
            }

            CurrentBehaviour?.Dispose();

            var stateInfo = new EnemyStateInfo(newState, lastEnemyState, _enemyStateChanged);
            CurrentBehaviour = _behaviourFactory.Create(
                stateInfo,
                _view,
                _enemyInput,
                _unitMovementModel,
                _enemyConfig,
                targetTransform);
        }
    }
}