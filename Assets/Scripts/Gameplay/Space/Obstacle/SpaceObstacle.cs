using SpaceRogue.Abstraction;
using SpaceRogue.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace SpaceRogue.Gameplay.Space.Obstacle
{
    public sealed class SpaceObstacle : IDisposable
    {

        #region Events

        public event Action PlayerInObstacle;

        public event Action PlayerOutObstacle;

        #endregion


        #region Fields

        private const int SEARCH_DISTANCE = 10000;

        private readonly Updater _updater;
        private readonly SpaceObstacleView _obstacleView;
        private readonly Collider2D _obstacleCollider;
        private readonly float _obstacleForce;

        private readonly Dictionary<EntityViewBase, Vector3> _unitCollection = new();

        private readonly List<EntityViewBase> _listForRemoving = new();

        #endregion


        #region CodeLife

        public SpaceObstacle(Updater updater, SpaceObstacleView obstacleView, float obstacleForce)
        {
            _updater = updater;
            _obstacleView = obstacleView;

            _obstacleCollider = obstacleView.TryGetComponent(out CompositeCollider2D compositeCollider2D)
                ? compositeCollider2D
                : obstacleView.GetComponent<Collider2D>();

            _obstacleForce = obstacleForce;

            _obstacleView.OnTriggerEnter += OnObstacleEnter;
            _obstacleView.OnTriggerExit += OnObstacleExit;

            _updater.SubscribeToFixedUpdate(Repulsion);
        }

        public void Dispose()
        {
            _obstacleView.OnTriggerEnter -= OnObstacleEnter;
            _obstacleView.OnTriggerExit -= OnObstacleExit;

            _unitCollection.Clear();
            _listForRemoving.Clear();

            _updater.UnsubscribeFromFixedUpdate(Repulsion);
        }

        #endregion


        #region Methods

        private void OnObstacleEnter(EntityViewBase entityView)
        {
            if (_unitCollection.ContainsKey(entityView))
            {
                return;
            }

            if (entityView.EntityType == Enums.EntityType.Player)
            {
                PlayerInObstacle?.Invoke();
            }

            var closestPoint = _obstacleCollider.ClosestPoint(entityView.transform.position);

            if (closestPoint == (Vector2)entityView.transform.position)
            {
                var searchPoint = entityView.transform.TransformPoint(Vector3.down * SEARCH_DISTANCE);
                closestPoint = _obstacleCollider.ClosestPoint(searchPoint);
            }

            _unitCollection.Add(entityView, closestPoint);
        }

        private void OnObstacleExit(EntityViewBase entityView)
        {
            if (!_unitCollection.ContainsKey(entityView))
            {
                return;
            }

            if (entityView.EntityType == Enums.EntityType.Player)
            {
                PlayerOutObstacle?.Invoke();
            }

            _unitCollection.Remove(entityView);
        }

        private void Repulsion()
        {
            if (!_unitCollection.Any())
            {
                return;
            }

            foreach (var item in _unitCollection)
            {
                if (item.Key == null)
                {
                    _listForRemoving.Add(item.Key);
                    continue;
                }

                var rigidbody = item.Key.Rigidbody2D;
                var position = rigidbody.transform.position;
                var anchorPoint = (Vector3)_obstacleCollider.ClosestPoint(position);
                var vectorDirection = anchorPoint == position ? position - item.Value : anchorPoint - position;
                anchorPoint += vectorDirection.normalized;
                var forceDirection = (position - anchorPoint).normalized;

                rigidbody.AddForce(forceDirection * _obstacleForce, ForceMode2D.Impulse);
            }

            foreach (var entityView in _listForRemoving)
            {
                _unitCollection.Remove(entityView);
            }

            if (_listForRemoving.Count > 0)
            {
                _listForRemoving.Clear();
            }
        }

        #endregion

    }
}