﻿using Abstracts;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gameplay.Space.Obstacle
{
    public sealed class SpaceObstacle : IDisposable
    {
        private const int SearchDistance = 10000;

        private readonly Updater _updater;
        private readonly SpaceObstacleView _obstacleView;
        private readonly Collider2D _obstacleCollider;
        private readonly float _obstacleForce;

        private readonly Dictionary<UnitView, Vector3> _unitCollection = new();

        public SpaceObstacle(Updater updater, SpaceObstacleView obstacleView, float obstacleForce)
        {
            _updater = updater;
            _obstacleView = obstacleView;
            
            if (obstacleView.TryGetComponent<CompositeCollider2D>(out var compositeCollider2D))
            {
                _obstacleCollider = compositeCollider2D;
            }
            else
            {
                _obstacleCollider = obstacleView.GetComponent<Collider2D>();
            }

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

            _updater.UnsubscribeFromFixedUpdate(Repulsion);
        }

        private void Repulsion()
        {
            if (!_unitCollection.Any())
            {
                return;
            }

            foreach (var item in _unitCollection)
            {
                var rigidbody = item.Key.GetComponent<Rigidbody2D>();
                var anchorPoint = (Vector3)_obstacleCollider.ClosestPoint(rigidbody.transform.position);
                var vectorDirection = anchorPoint - rigidbody.transform.position;

                if (anchorPoint == rigidbody.transform.position)
                {
                    vectorDirection = rigidbody.transform.position - item.Value;
                }

                anchorPoint += vectorDirection.normalized;
                var forceDirection = (rigidbody.transform.position - anchorPoint).normalized;
                rigidbody.AddForce(forceDirection * _obstacleForce, ForceMode2D.Impulse);
            }
        }

        private void OnObstacleEnter(UnitView unitView)
        {
            if (_unitCollection.ContainsKey(unitView))
            {
                return;
            }

            var closestPoint = _obstacleCollider.ClosestPoint(unitView.transform.position);
            
            if (closestPoint == (Vector2)unitView.transform.position)
            {
                var searchPoint = unitView.transform.TransformPoint(Vector3.down * SearchDistance);
                closestPoint = _obstacleCollider.ClosestPoint(searchPoint);
            }

            _unitCollection.Add(unitView, closestPoint);
        }

        private void OnObstacleExit(UnitView unitView)
        {
            if (!_unitCollection.ContainsKey(unitView))
            {
                return;
            }
            _unitCollection.Remove(unitView);
        }
    }
}