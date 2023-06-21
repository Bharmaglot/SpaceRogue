using Gameplay.Shooting.Factories;
using Gameplay.Shooting.Scriptables;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using SpaceRogue.Abstraction;
using SpaceRogue.Enums;
using SpaceRogue.Services;


namespace Gameplay.Shooting
{
    public sealed class TurretMountedWeapon : MountedWeapon, IDisposable
    {
        private readonly Updater _updater;

        private readonly TurretView _turretView;
        private readonly Transform _gunPointViewTransform;

        private readonly float _rotationSpeed;

        private readonly List<EntityView> _targets;
        private EntityView _currentTarget;
        private readonly EntityType _entityType;

        public TurretMountedWeapon(Weapon weapon, EntityView entityView, TurretViewFactory turretViewFactory, GunPointViewFactory gunPointViewFactory, TurretConfig config, Updater updater) : base(weapon, entityView)
        {
            _updater = updater;

            var unitScale = UnitViewTransform.localScale;
            var gunPointPosition = UnitViewTransform.position + UnitViewTransform.TransformDirection(0.6f * Mathf.Max(unitScale.x, unitScale.y) * Vector3.up);
            var turretView = turretViewFactory.Create(UnitViewTransform, config);
            _turretView = turretView;
            var gunPoint = gunPointViewFactory.Create(gunPointPosition, UnitViewTransform.rotation, _turretView.transform);
            _gunPointViewTransform = gunPoint.transform;
            _entityType = entityView.EntityType;
            _rotationSpeed = config.TurningSpeed;

            _targets = new List<EntityView>();

            _turretView.TargetEntersTrigger += OnTargetInRange;
            _turretView.TargetExitsTrigger += OnTargetOutOfRange;
        }

        public override void CommenceFiring()
        {
            Weapon.CommenceFiring(_gunPointViewTransform.position, _gunPointViewTransform.rotation);
        }

        private void RotateTurret()
        {
            if (_currentTarget is null) return;
            var direction = _currentTarget.transform.position - _turretView.transform.position;
            _turretView.Rotate(direction, _rotationSpeed);
        }

        private EntityView PickNewTarget()
        {
            if (!_targets.Any()) return null;
            if (_targets.Count == 1) return _targets[0];
            return _targets.OrderBy(t => (_turretView.transform.position - t.transform.position).sqrMagnitude).First();
        }

        private void OnTargetInRange(EntityView target)
        {
            if (_entityType == EntityType.Player)
            {
                if (target.EntityType == EntityType.Enemy | target.EntityType == EntityType.EnemyAssistant)
                {
                    _targets.Add(target);
                    if (_currentTarget is null)
                    {
                        _currentTarget = target;
                        _currentTarget.EntityDestroyed += OnTargetIsDestioyed;
                        _updater.SubscribeToUpdate(RotateTurret);
                    }
                }
            }

            if (_entityType == EntityType.Enemy)
            {
                if (target.EntityType == EntityType.Player)
                {
                    _currentTarget = target;
                    _currentTarget.EntityDestroyed += OnTargetIsDestioyed;
                    _updater.SubscribeToUpdate(RotateTurret);
                }
            }
        }

        private void OnTargetOutOfRange(EntityView target)
        {
            if (_entityType == EntityType.Player)
            {
                if (target.EntityType == EntityType.Enemy | target.EntityType == EntityType.EnemyAssistant)
                {
                    _targets.Remove(target);
                    if (_currentTarget == target)
                    {
                        _currentTarget.EntityDestroyed -= OnTargetIsDestioyed;
                        _currentTarget = PickNewTarget();
                        if (_currentTarget is null) _updater.UnsubscribeFromUpdate(RotateTurret);
                        else _currentTarget.EntityDestroyed += OnTargetIsDestioyed;
                    }
                }
            }

            if (_entityType == EntityType.Enemy)
            {
                if (target.EntityType == EntityType.Player)
                {
                    _currentTarget.EntityDestroyed -= OnTargetIsDestioyed;
                    _updater.UnsubscribeFromUpdate(RotateTurret);
                    _currentTarget = null;
                }
            }
        }

        public void Dispose()
        {
            _updater.UnsubscribeFromUpdate(RotateTurret);
            _turretView.TargetEntersTrigger -= OnTargetInRange;
            _turretView.TargetExitsTrigger -= OnTargetOutOfRange;
            _currentTarget.EntityDestroyed -= OnTargetIsDestioyed;
        }

        private void OnTargetIsDestioyed()
        {
            _updater.UnsubscribeFromUpdate(RotateTurret);
        }
    }
}