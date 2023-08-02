using SpaceRogue.Abstraction;
using SpaceRogue.Enums;
using SpaceRogue.Gameplay.Shooting.Factories;
using SpaceRogue.Gameplay.Shooting.Scriptables;
using SpaceRogue.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace SpaceRogue.Gameplay.Shooting
{
    public sealed class TurretMountedWeapon : MountedWeapon, IDisposable
    {

        #region Fields

        private readonly Updater _updater;
        private readonly TurretView _turretView;
        private readonly Transform _gunPointViewTransform;
        private readonly EntityType _entityType;
        private readonly float _rotationSpeed;

        private readonly List<EntityViewBase> _targets;

        private EntityViewBase _currentTarget;

        #endregion


        #region CodeLife

        public TurretMountedWeapon(
            Weapon weapon,
            EntityViewBase entityView,
            TurretViewFactory turretViewFactory,
            GunPointViewFactory gunPointViewFactory,
            TurretConfig config,
            Updater updater)
            : base(weapon, entityView)
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

            _targets = new List<EntityViewBase>();

            _turretView.TargetEntersTrigger += OnTargetInRange;
            _turretView.TargetExitsTrigger += OnTargetOutOfRange;
        }

        public void Dispose()
        {
            _updater.UnsubscribeFromUpdate(RotateTurret);
            _turretView.TargetEntersTrigger -= OnTargetInRange;
            _turretView.TargetExitsTrigger -= OnTargetOutOfRange;
            _currentTarget.EntityDestroyed -= OnTargetIsDestroyed;
            if(_currentTarget.EntityType == EntityType.Player)
            {
                _updater.UnsubscribeFromUpdate(CommenceFiring);
            }
        }

        #endregion


        #region Methods

        public override void CommenceFiring() => Weapon.CommenceFiring(_gunPointViewTransform.position, _gunPointViewTransform.rotation);

        private void RotateTurret()
        {
            if (_currentTarget == null)
            {
                return;
            }

            var direction = _currentTarget.transform.position - _turretView.transform.position;
            _turretView.Rotate(direction, _rotationSpeed);
        }

        private EntityViewBase PickNewTarget() => !_targets.Any()
                ? null
                : _targets.Count == 1
                ? _targets[0]
                : _targets.OrderBy(t => (_turretView.transform.position - t.transform.position).sqrMagnitude).First();

        private void OnTargetInRange(EntityViewBase target)
        {
            if (_entityType == EntityType.Player)
            {
                if (target.EntityType == EntityType.Enemy | target.EntityType == EntityType.EnemyAssistant)
                {
                    _targets.Add(target);
                    if (_currentTarget == null)
                    {
                        _currentTarget = target;
                        _currentTarget.EntityDestroyed += OnTargetIsDestroyed;
                        _updater.SubscribeToUpdate(RotateTurret);
                    }
                }
            }

            if (_entityType == EntityType.Enemy)
            {
                if (target.EntityType == EntityType.Player)
                {
                    _currentTarget = target;
                    _currentTarget.EntityDestroyed += OnTargetIsDestroyed;
                    _updater.SubscribeToUpdate(RotateTurret);
                    _updater.SubscribeToUpdate(CommenceFiring);
                }
            }
        }

        private void OnTargetOutOfRange(EntityViewBase target)
        {
            if (_entityType == EntityType.Player)
            {
                if (target.EntityType == EntityType.Enemy | target.EntityType == EntityType.EnemyAssistant)
                {
                    _targets.Remove(target);
                    if (_currentTarget == target)
                    {
                        _currentTarget.EntityDestroyed -= OnTargetIsDestroyed;
                        _currentTarget = PickNewTarget();
                        if (_currentTarget == null)
                        {
                            _updater.UnsubscribeFromUpdate(RotateTurret);
                        }
                        else
                        {
                            _currentTarget.EntityDestroyed += OnTargetIsDestroyed;
                        }
                    }
                }
            }

            if (_entityType == EntityType.Enemy)
            {
                if (target.EntityType == EntityType.Player)
                {
                    _currentTarget.EntityDestroyed -= OnTargetIsDestroyed;
                    _updater.UnsubscribeFromUpdate(RotateTurret);
                    _updater.UnsubscribeFromUpdate(CommenceFiring);
                    _currentTarget = null;
                }
            }
        }

        private void OnTargetIsDestroyed() => _updater.UnsubscribeFromUpdate(RotateTurret);

        #endregion

    }
}