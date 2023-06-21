using Gameplay.Mechanics.Timer;
using Gameplay.Movement;
using SpaceRogue.Abstraction;
using System;
using UnityEngine;


namespace SpaceRogue.Player.Movement
{
    public sealed class UnitMovement : IDisposable
    {

        #region Fields

        private readonly Transform _transform;
        private readonly Rigidbody2D _rigidbody;
        private readonly IUnitMovementInput _movementInput;
        private readonly UnitMovementModel _model;

        private Timer _dashTimer;

        #endregion


        #region Properties

        public float CurrentSpeed => _model.CurrentSpeed;
        public float MaxSpeed => _model.MaxSpeed;

        #endregion


        #region CodeLife

        public UnitMovement(
            EntityViewBase entityView,
            IUnitMovementInput movementInput,
            UnitMovementModel model,
            TimerFactory timerFactory)
        {
            _rigidbody = entityView.GetComponent<Rigidbody2D>();
            _transform = entityView.transform;
            _movementInput = movementInput;
            _model = model;

            _dashTimer = timerFactory.Create(_model.UnitMovementConfig.DashCooldown);

            _movementInput.VerticalAxisInput += HandleVerticalInput;
            _movementInput.HorizontalAxisInput += HorizontalAxisInputHandler;
        }

        public void Dispose()
        {
            _movementInput.VerticalAxisInput -= HandleVerticalInput;
            _movementInput.HorizontalAxisInput -= HorizontalAxisInputHandler;

            _dashTimer.Dispose();
        }

        #endregion


        #region Methods

        private void HorizontalAxisInputHandler(float axisValue)
        {
            if (Mathf.Approximately(axisValue, 0.0f))
            {
                return;
            }

            if (!_dashTimer.IsExpired)
            {
                return;
            }

            _dashTimer.Start();

            var direction = axisValue < 0.0f ? Vector2.left : Vector2.right;
            var force = _rigidbody.mass * Mathf.Sqrt(_model.UnitMovementConfig.DashLength * Physics.gravity.magnitude) * direction;

            _rigidbody.AddForce(force, ForceMode2D.Impulse);
        }

        private void HandleVerticalInput(float newInputValue)
        {
            bool isZeroInput = Mathf.Approximately(newInputValue, 0.0f);

            if (!isZeroInput)
            {
                _model.Accelerate(newInputValue > 0.0f);
            }

            if (!Mathf.Approximately(CurrentSpeed, 0.0f))
            {
                var forwardDirection = _transform.TransformDirection(Vector3.up);
                _rigidbody.AddForce(forwardDirection.normalized * CurrentSpeed, ForceMode2D.Force);
            }

            if (_rigidbody.velocity.sqrMagnitude > MaxSpeed * MaxSpeed)
            {
                Vector3 velocity = _rigidbody.velocity.normalized * MaxSpeed;
                _rigidbody.velocity = velocity;
            }

            if (isZeroInput && CurrentSpeed < _model.StoppingSpeed && CurrentSpeed > -_model.StoppingSpeed)
            {
                _rigidbody.velocity = Vector2.Lerp(_rigidbody.velocity, Vector2.zero, Time.deltaTime);
                _model.StopMoving();
            }
        }

        #endregion

    }
}