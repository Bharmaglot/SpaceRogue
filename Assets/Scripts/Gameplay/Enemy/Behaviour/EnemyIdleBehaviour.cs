using Gameplay.Movement;
using Gameplay.Services;
using SpaceRogue.Enemy.Movement;
using SpaceRogue.Services;
using System;
using UnityEngine;


namespace Gameplay.Enemy.Behaviour
{
    public sealed class EnemyIdleBehaviour : EnemyBehaviour
    {
        public EnemyIdleBehaviour(
            Updater updater,
            PlayerLocator playerLocator,
            EnemiesAlarm enemiesAlarm,
            EnemyState state,
            Action<EnemyState> enemyStateChanged,
            EnemyView view,
            EnemyInput input,
            UnitMovementModel model,
            EnemyBehaviourConfig config,
            Transform targetTransform) 
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
        }

        protected override void OnUpdate() { }
    }
}