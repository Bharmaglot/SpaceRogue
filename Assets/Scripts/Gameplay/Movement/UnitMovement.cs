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

        private Timer _dashCooldownTimer;
        private Timer _dashTravelTimer;

        private Vector2 _dashForce;
        private Vector2 _dashPreviousPosition;

        private float _dashRemainingDistance;

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

            _dashCooldownTimer = timerFactory.Create(_model.UnitMovementConfig.DashCooldown);
            _dashTravelTimer = timerFactory.Create(_model.UnitMovementConfig.DashCompleteTime);

            _movementInput.VerticalAxisInput += HandleVerticalInput;
            _movementInput.HorizontalAxisInput += HorizontalAxisInputHandler;
        }

        public void Dispose()
        {
            _movementInput.VerticalAxisInput -= HandleVerticalInput;
            _movementInput.HorizontalAxisInput -= HorizontalAxisInputHandler;

            _dashCooldownTimer.Dispose();
        }

        #endregion


        #region Methods

        private void HorizontalAxisInputHandler(float axisValue)
        {
            if (Mathf.Approximately(axisValue, 0.0f))
            {
                return;
            }

            if (!_dashCooldownTimer.IsExpired)
            {
                return;
            }

            _dashCooldownTimer.Start();

            var direction = axisValue < 0.0f ? Vector2.left : Vector2.right;

            if (_transform.up.y < 0.0f)
            {
                direction = -direction;
            }

            // This is correct, but don't work
            //var force = _rigidbody.mass * Mathf.Sqrt(2 * _model.UnitMovementConfig.DashLength * Physics2D.gravity.magnitude) * direction;

            // This is correct, but don't work
            //var travelTime = _model.UnitMovementConfig.DashLength / _model.UnitMovementConfig.maximumSpeed;// Time to stop. Need speed
            //var force = _rigidbody.mass * _model.UnitMovementConfig.DashLength * direction / travelTime;

            // Correct for distance less 28-30 units
            //var force = _rigidbody.mass * Mathf.Pow(_model.UnitMovementConfig.DashLength, 3) / Mathf.Pow(Physics2D.gravity.magnitude, 2) * direction;

            //var physicsCorrectionFactor = _model.UnitMovementConfig.DashLength > 50.0f ? 2 : 1;
            //_dashForce = _rigidbody.mass * _model.UnitMovementConfig.DashLength * direction / Mathf.Pow(_model.UnitMovementConfig.DashCompleteTime * physicsCorrectionFactor, 2.0f);

            _dashForce = _rigidbody.mass * 1000.0f * direction;
            _dashRemainingDistance = _model.UnitMovementConfig.DashLength;

            _dashTravelTimer.OnTick += OnTickTimerHandler;
            _dashTravelTimer.OnExpire += DashCompleteHandler;
            _dashTravelTimer.Start();
            _dashPreviousPosition = _rigidbody.position;
        }

        private void DashCompleteHandler()
        {
            _dashTravelTimer.OnTick -= OnTickTimerHandler;
        }

        private void OnTickTimerHandler()
        {
            //_dashForce = Vector2.Lerp(_dashForce, Vector2.zero, 1.0f - _dashTravelTimer.CurrentValue / _model.UnitMovementConfig.DashCompleteTime);
            Debug.Log("t = " + _dashTravelTimer.CurrentValue);
            //Debug.Log("F = " + _dashForce);
            _rigidbody.AddRelativeForce(_dashForce);
            var elapsedDistance = Vector2.Distance(_dashPreviousPosition, _rigidbody.position);
            _dashPreviousPosition = _rigidbody.position;
            _dashRemainingDistance -= elapsedDistance;
            
            if (_dashRemainingDistance <= 0.0f)
            {
                _rigidbody.AddRelativeForce(-_dashForce);
                DashCompleteHandler();
            }
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