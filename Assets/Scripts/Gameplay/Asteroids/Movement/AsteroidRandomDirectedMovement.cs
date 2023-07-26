using UnityEngine;
using Utilities.Mathematics;

namespace Gameplay.Asteroids.Movement
{
    public sealed class AsteroidRandomDirectedMovement : IAsteroidMovementBehaviour
    {
        private readonly AsteroidView _view;
        private readonly float _startingSpeed;

        public AsteroidRandomDirectedMovement(float startingSpeed, AsteroidView view)
        {
            _view = view;
            _startingSpeed = startingSpeed;
        }

        public void StartMovement()
        {
            var rigidbody = _view.Rigidbody2D;
            var direction = RandomPicker.PickRandomAngle(0, 360);
            rigidbody.AddForce(direction * _startingSpeed, ForceMode2D.Impulse);
        }
    }
}