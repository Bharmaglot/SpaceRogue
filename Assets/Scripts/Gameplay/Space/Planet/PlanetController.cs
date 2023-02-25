using Abstracts;
using Gameplay.Damage;
using Gameplay.Space.Star;
using UnityEngine;

namespace Gameplay.Space.Planet
{
    public sealed class PlanetController : BaseController
    {
        private readonly PlanetView _view;
        private readonly float _currentSpeed;
        private readonly bool _isMovingRetrograde;
        
        private readonly SpaceObjectView _spaceObjectView;

        public PlanetController(PlanetView view, SpaceObjectView spaceObjectView, float speed, bool isMovingRetrograde, float planetDamage)
        {
            _view = view;
            _view.transform.parent = spaceObjectView.transform;

            var damageModel = new DamageModel(planetDamage);
            view.Init(damageModel);

            AddGameObject(view.gameObject);
            _spaceObjectView = spaceObjectView;
            _currentSpeed = speed;
            _isMovingRetrograde = isMovingRetrograde;
            _view.CollisionEnter += Dispose;

            EntryPoint.SubscribeToUpdate(Move);
        }

        protected override void OnDispose()
        {
            _view.CollisionEnter -= Dispose;
            EntryPoint.UnsubscribeFromUpdate(Move);
        }

        private void Move(float deltaTime)
        {
            if (_spaceObjectView is not null)
            {
                _view.transform.RotateAround(
                    _spaceObjectView.transform.position,
                    _isMovingRetrograde ? Vector3.forward : Vector3.back,
                    _currentSpeed * deltaTime
                );
            }
        }
    }
}