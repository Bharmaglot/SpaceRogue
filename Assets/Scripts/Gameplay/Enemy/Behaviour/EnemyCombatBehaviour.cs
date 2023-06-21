using System;
using Gameplay.Movement;
using Gameplay.Services;
using SpaceRogue.Enemy.Movement;
using SpaceRogue.Services;
using UnityEngine;
using Utilities.Unity;


namespace Gameplay.Enemy.Behaviour
{
    public class EnemyCombatBehaviour : EnemyBehaviour
    {
        private Vector3 _currentDirection;

        public EnemyCombatBehaviour(
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

        protected override void OnLosingTarget() { }

        protected override void OnTargetInZone()
        {
            var directionInsideAngle = UnityHelper.DirectionInsideAngle(
                MovementDirection,
                _currentDirection,
                Config.FiringAngle);

            if (directionInsideAngle)
            {
                Input.Fire();
            }
        }

        protected override void OnUpdate()
        {
            if (TargetTransform == null)
            {
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
                (TargetTransform.position - View.transform.position).normalized;
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
                TargetTransform.position,
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