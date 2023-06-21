using System;
using Gameplay.Movement;
using Gameplay.Services;
using SpaceRogue.Enemy.Movement;
using SpaceRogue.Services;
using UnityEngine;
using Zenject;


namespace Gameplay.Enemy.Behaviour
{
    public sealed class EnemyBehaviourFactory : IFactory<EnemyStateInfo, EnemyView, EnemyInput, UnitMovementModel, EnemyBehaviourConfig, Transform, EnemyBehaviour>
    {
        private readonly Updater _updater;
        private readonly PlayerLocator _playerLocator;
        private readonly EnemiesAlarm _enemiesAlarm;

        public EnemyBehaviourFactory(
            Updater updater,
            PlayerLocator playerLocator,
            EnemiesAlarm enemiesAlarm)
        {
            _updater = updater;
            _playerLocator = playerLocator;
            _enemiesAlarm = enemiesAlarm;
        }

        public EnemyBehaviour Create(
            EnemyStateInfo stateInfo,
            EnemyView view,
            EnemyInput input,
            UnitMovementModel model,
            EnemyBehaviourConfig behaviourConfig,
            Transform targetTransform)
        {
            return stateInfo.CurrentState switch
            {
                EnemyState.Idle => 
                new EnemyIdleBehaviour(
                    _updater,
                    _playerLocator,
                    _enemiesAlarm,
                    stateInfo.CurrentState,
                    stateInfo.StateChanged,
                    view,
                    input,
                    model,
                    behaviourConfig,
                    targetTransform),

                EnemyState.PassiveRoaming => 
                new EnemyRoamingBehaviour(
                    _updater,
                    _playerLocator,
                    _enemiesAlarm,
                    stateInfo.CurrentState,
                    stateInfo.StateChanged,
                    view,
                    input,
                    model,
                    behaviourConfig),

                EnemyState.InCombat => 
                new EnemyCombatBehaviour(
                    _updater,
                    _playerLocator,
                    _enemiesAlarm,
                    stateInfo.CurrentState,
                    stateInfo.StateChanged,
                    view,
                    input,
                    model,
                    behaviourConfig,
                    targetTransform),

                EnemyState.InCombatWithRetreat => 
                new EnemyCombatWithRetreatBehaviour(
                    _updater,
                    _playerLocator,
                    _enemiesAlarm,
                    stateInfo.CurrentState,
                    stateInfo.StateChanged,
                    view,
                    input,
                    model,
                    behaviourConfig,
                    targetTransform,
                    stateInfo.LastState),

                EnemyState.Escort => 
                new EnemyEscortBehaviour(
                    _updater,
                    _playerLocator,
                    _enemiesAlarm,
                    stateInfo.CurrentState,
                    stateInfo.StateChanged,
                    view,
                    input,
                    model,
                    behaviourConfig,
                    targetTransform),

                _ => throw new NotImplementedException()
            };
        }
    }
}