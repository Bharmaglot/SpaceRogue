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
        private Vector2 _dashStartVelocity;

        private float _dashRemainingDistance;

        #endregion


        #region Properties

        public float ExtraSpeed { get; set; } = 0.0f;
        public float CurrentSpeed => _model.CurrentSpeed + ExtraSpeed;
        public float MaxSpeed => _model.MaxSpeed + ExtraSpeed;

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
            _dashTravelTimer = timerFactory.Create(_model.UnitMovementConfig.DashCooldown);

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

            CalculateForce(_model.UnitMovementConfig.DashLength, out var force, out _dashRemainingDistance);
            _dashForce = _rigidbody.mass * force * direction;

            _dashTravelTimer.OnTick += OnTickTimerHandler;
            _dashTravelTimer.OnExpire += DashCompleteHandler;
            _dashPreviousPosition = _rigidbody.position;
            _dashStartVelocity = _rigidbody.velocity;
            _dashTravelTimer.Start();
        }

        /// <summary>
        /// This is a cubic regression formula that calculates the applied force to move a given distance
        /// </summary>
        /// <param name="dashLength">Distance for dash</param>
        /// <param name="forceMagnitude">Magnitude of force for using for dash</param>
        /// <param name="stopDistance">Traveled distance after which the force must stop</param>
        private void CalculateForce(float dashLength, out float forceMagnitude, out float stopDistance)
        {
            var stopFactor = 0.5f;

            var x = stopFactor * dashLength;
            var y = 0.431f * Mathf.Pow(x, 3) - 9.320f * Mathf.Pow(x, 2) + 102.012f * x - 99.366f;

            forceMagnitude = y;
            stopDistance = stopFactor * dashLength;
        }

        private void DashCompleteHandler()
        {
            _rigidbody.velocity = _dashStartVelocity;
            _dashTravelTimer.OnTick -= OnTickTimerHandler;
            _dashTravelTimer.OnExpire -= DashCompleteHandler;
        }

        private void OnTickTimerHandler()
        {
            _rigidbody.AddRelativeForce(_dashForce);

            var elapsedDistance = Vector2.Distance(_dashPreviousPosition, _rigidbody.position);
            _dashPreviousPosition = _rigidbody.position;
            _dashRemainingDistance -= elapsedDistance;

            if (_dashRemainingDistance <= 0.0f)
            {
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