using System;
using Gameplay.Asteroids.Movement;
using Gameplay.Survival;
using Object = UnityEngine.Object;

namespace Gameplay.Asteroids
{
    public class Asteroid : IDisposable
    {
        private readonly IAsteroidMovementBehaviour _asteroidMovement;
        private readonly EntitySurvival _survival;
        private readonly AsteroidView _view;

        private bool _disposing;
        
        public Asteroid(AsteroidView view, IAsteroidMovementBehaviour asteroidMovement, EntitySurvival survival)
        {
            _asteroidMovement = asteroidMovement;
            _survival = survival;
            _view = view;

            _survival.UnitDestroyed += Dispose;
        }

        public void Dispose()
        {
            if (_disposing) return;
            _disposing = true;
            
            _survival.UnitDestroyed -= Dispose;
            _survival.Dispose();

            if (_view is not null)
            {
                Object.Destroy(_view.gameObject);
            }
        }
    }
}