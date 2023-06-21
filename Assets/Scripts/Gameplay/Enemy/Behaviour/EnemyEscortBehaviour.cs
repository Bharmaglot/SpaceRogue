using Gameplay.Movement;
using Gameplay.Services;
using SpaceRogue.Enemy.Movement;
using SpaceRogue.Services;
using System;
using UnityEngine;
using Utilities.Unity;


namespace Gameplay.Enemy.Behaviour
{
    public sealed class EnemyEscortBehaviour : EnemyBehaviour
    {
        private readonly Transform _escortTarget;

        private Vector3 _currentDirection;

        public EnemyEscortBehaviour(
            Updater updater,
            PlayerLocator playerLocator,
            EnemiesAlarm enemiesAlarm,
            EnemyState state,
            Action<EnemyState> enemyStateChanged,
            EnemyView view,
            EnemyInput input,
            UnitMovementModel model,
            EnemyBehaviourConfig config,
            Transform escortTarget)
            : base(
                  updater,
                  playerLocator,
                  enemiesAlarm,
                  state,
                  enemyStateChanged,
                  view,
                  input,
                  model,
                  config)
        {
            _escortTarget = escortTarget;
        }

        protected override void OnTargetInZone()
        {
            ChangeState(EnemyState.InCombatWithRetreat);
        }

        protected override void OnAcceptAlarm()
        {
            ChangeState(EnemyState.InCombatWithRetreat);
        }

        protected override void OnUpdate()
        {
            if (_escortTarget == null)
            {
                ChangeState(EnemyState.PassiveRoaming);
                return;
            }

            GetDirectionsAndDistance();
            RotateTowardsPlayer();
            Move();
        }

        private void GetDirectionsAndDistance()
        {
            _currentDirection = View.transform.TransformDirection(Vector3.up);
            MovementDirection = 
                (_escortTarget.position - View.transform.position).normalized;
        }

        private void RotateTowardsPlayer()
        {
            var turnOrNot = UnityHelper.Approximately(
                MovementDirection,
                _currentDirection,
                0.1f);

            if (turnOrNot)
            {
                Input.StopTurning();
            }
            else
            {
                var turningLeft = UnityHelper.VectorAngleLessThanAngle(
                    MovementDirection,
                    _currentDirection,
                    0);

                HandleTurn(turningLeft);
            }
        }

        private void HandleTurn(bool turningLeft)
        {
            if (turningLeft)
            {
                Input.TurnLeft();
            }
            else
            {
                Input.TurnRight();
            }
        }

        private void Move()
        {
            var inDetectionRadius = UnityHelper.InDetectionRadius(
                View.transform.position,
                _escortTarget.position,
                Config.ApproachDistance);

            if (inDetectionRadius)
            {
                Input.Decelerate();
            }
            else
            {
                Input.Accelerate();
            }
        }
    }
}