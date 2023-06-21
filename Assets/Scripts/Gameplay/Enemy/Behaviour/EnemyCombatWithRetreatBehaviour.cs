using Gameplay.Enemy.Movement;
using Gameplay.Movement;
using Gameplay.Services;
using SpaceRogue.Services;
using System;
using UnityEngine;


namespace Gameplay.Enemy.Behaviour
{
    public sealed class EnemyCombatWithRetreatBehaviour : EnemyCombatBehaviour
    {
        private readonly EnemyState _lastEnemyState;

        public EnemyCombatWithRetreatBehaviour(
            Updater updater,
            PlayerLocator playerLocator,
            EnemiesAlarm enemiesAlarm,
            EnemyState state,
            Action<EnemyState> enemyStateChanged,
            EnemyView view,
            EnemyInput input,
            UnitMovementModel model,
            EnemyBehaviourConfig config,
            Transform targetTransform,
            EnemyState lastEnemyState)
            : base(
                  updater,
                  playerLocator,
                  enemiesAlarm,
                  state,
                  enemyStateChanged,
                  view,
                  input,
                  model,
                  config,
                  targetTransform)
        {
            _lastEnemyState = lastEnemyState;
        }

        protected override void OnLosingTarget()
        {
            ChangeState(_lastEnemyState);
        }
    }
}