using Gameplay.Movement;
using System;
using UnityEngine;

namespace Gameplay.Enemy.Movement
{
    public sealed class EnemyTurning : IDisposable
    {
        private readonly Transform _transform;
        private readonly Rigidbody2D _rigidbody;
        private readonly EnemyInput _enemyInput;
        private readonly UnitMovementModel _model;

        public EnemyTurning(
            EnemyView enemyView,
            EnemyInput enemyInput,
            UnitMovementModel model)
        {
            _transform = enemyView.transform;
            _rigidbody = enemyView.GetComponent<Rigidbody2D>();
            _enemyInput = enemyInput;
            _model = model;

            _enemyInput.HorizontalAxisInput += HandleHorizontalInput;
        }

        public void Dispose()
        {
            _enemyInput.HorizontalAxisInput -= HandleHorizontalInput;
        }
        
        private void HandleHorizontalInput(float newInputValue)
        {
            Quaternion newRotation = Quaternion.identity;
            switch (newInputValue)
            {
                case 0:
                    _model.StopTurning();
                    newRotation = _transform.rotation;
                    break;
                case < 0:
                    _model.Turn(true);
                    newRotation = _transform.rotation * Quaternion.AngleAxis(_model.CurrentTurnRate * newInputValue, Vector3.forward);
                    break;
                case > 0:
                    _model.Turn(false);
                    newRotation = _transform.rotation * Quaternion.AngleAxis(_model.CurrentTurnRate * newInputValue, Vector3.back);
                    break;
            }
            _rigidbody.MoveRotation(newRotation);
        }
    }
}