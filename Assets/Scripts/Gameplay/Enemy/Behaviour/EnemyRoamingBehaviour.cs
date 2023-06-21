using System;
using Gameplay.Enemy.Movement;
using Gameplay.Movement;
using Gameplay.Services;
using SpaceRogue.Abstraction;
using SpaceRogue.Services;
using UnityEngine;
using Utilities.Unity;


namespace Gameplay.Enemy.Behaviour
{
    public sealed class EnemyRoamingBehaviour : EnemyBehaviour
    {
        private const float TurnValueAtObstacle = 0.1f;
        
        private bool _frontObstacle;
        private bool _rightObstacle;
        private bool _leftObstacle;

        public EnemyRoamingBehaviour(
            Updater updater,
            PlayerLocator playerLocator,
            EnemiesAlarm enemiesAlarm,
            EnemyState state,
            Action<EnemyState> enemyStateChanged,
            EnemyView view,
            EnemyInput input,
            UnitMovementModel model,
            EnemyBehaviourConfig config) 
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
        }

        public override void SetMovementDirection(Vector3 direction)
        {
            MovementDirection = View.transform.TransformDirection(direction).normalized;
        }

        protected override void OnUpdate()
        {
            CheckObstacles();
            MoveAtLowSpeed(_frontObstacle);
            TurnToDirection(_rightObstacle, _leftObstacle);
        }

        private void CheckObstacles()
        {
            var position = View.transform.position;
            var scaleY = View.transform.localScale.y;
            var scaleX = View.transform.localScale.x;

            var rayUpPosition = 
                position + View.transform.TransformDirection(Vector3.up * scaleY);
            var rayRightPosition = 
                position + View.transform.TransformDirection(Vector3.right * scaleX);
            var rayLeftPosition = 
                position + View.transform.TransformDirection(Vector3.left * scaleX);

            var hitUp = Physics2D.Raycast(
                rayUpPosition,
                View.transform.TransformDirection(Vector3.up),
                Config.FrontCheckDistance);
            var hitRight = Physics2D.Raycast(
                rayRightPosition,
                View.transform.TransformDirection(Vector3.right),
                Config.SideCheckDistance);
            var hitLeft = Physics2D.Raycast(
                rayLeftPosition,
                View.transform.TransformDirection(Vector3.left),
                Config.SideCheckDistance);

            _frontObstacle = hitUp.collider != null 
                && !hitUp.collider.TryGetComponent<EntityView>(out _);
            _rightObstacle = hitRight.collider != null 
                && !hitRight.collider.TryGetComponent<EntityView>(out _);
            _leftObstacle = hitLeft.collider != null 
                && !hitLeft.collider.TryGetComponent<EntityView>(out _);
        }

        private void MoveAtLowSpeed(bool frontObstacle)
        {
            if (frontObstacle)
            {
                Input.Decelerate();
                return;
            }

            var quarterMaxSpeed = Model.MaxSpeed / 4;
            switch (CompareSpeeds(Model.CurrentSpeed, quarterMaxSpeed))
            {
                case -1: { Input.Accelerate(); return; }
                case 0: { Input.HoldSpeed(); return; }
                case 1: { Input.Decelerate(); return; }
            }
        }

        private int CompareSpeeds(float currentSpeed, float targetSpeed)
        {
            if (UnityHelper.Approximately(currentSpeed, targetSpeed, 0.1f)) return 0;
            if (currentSpeed < targetSpeed) return -1;
            return 1;
        }

        private void TurnToDirection(bool rightObstacle, bool leftObstacle)
        {
            if (rightObstacle)
            {
                Input.TurnLeft(TurnValueAtObstacle);
            }

            if (leftObstacle)
            {
                Input.TurnRight(TurnValueAtObstacle);
            }

            if (rightObstacle || leftObstacle)
            {
                return;
            }

            var currentDirection = View.transform.TransformDirection(Vector3.up);

            var turnOrNot = UnityHelper.Approximately(
                MovementDirection,
                currentDirection,
                0.1f);

            if (turnOrNot)
            {
                Input.StopTurning();
            }
            else
            {
                var turningLeft = UnityHelper.VectorAngleLessThanAngle(
                    MovementDirection,
                    currentDirection,
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
    }
}