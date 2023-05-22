using Gameplay.Abstracts;
using System;
using Gameplay.Shooting.Factories;
using Gameplay.Shooting.Scriptables;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Services;
using Gameplay.Enemy;

namespace Gameplay.Shooting
{
    public sealed class TurretMountedWeapon : MountedWeapon, IDisposable
    {
        private readonly Updater _updater;

        private readonly TurretView _turretView;
        private readonly Transform _gunPointViewTransform;

        private readonly float _rotationSpeed;

        private readonly List<EnemyView> _targets;
        private EnemyView _currentTarget;

        public TurretMountedWeapon(Weapon weapon, EntityView entityView, TurretViewFactory turretViewFactory, GunPointViewFactory gunPointViewFactory, TurretConfig config, Updater updater) : base(weapon, entityView)
        {
            _updater = updater;
            
            var unitScale = UnitViewTransform.localScale;
            var gunPointPosition = UnitViewTransform.position + UnitViewTransform.TransformDirection(0.6f * Mathf.Max(unitScale.x, unitScale.y) * Vector3.up);
            var turretView = turretViewFactory.Create(UnitViewTransform, config);
            _turretView = turretView;
            var gunPoint = gunPointViewFactory.Create(gunPointPosition, UnitViewTransform.rotation, _turretView.transform);
            _gunPointViewTransform = gunPoint.transform;
            
            _rotationSpeed = config.TurningSpeed;
            
            _targets = new List<EnemyView>();

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
        

        private EnemyView PickNewTarget()
        {
            if (!_targets.Any()) return null;
            if (_targets.Count == 1) return _targets[0];
            return _targets.OrderBy(t => (_turretView.transform.position - t.transform.position).sqrMagnitude).First();
        }

        private void OnTargetInRange(EnemyView target)
        {
            _targets.Add(target);
            if (_currentTarget is null)
            {
                _currentTarget = target;
                _currentTarget.EntityDestroyed += OnTargetIsDestioyed;
                _updater.SubscribeToUpdate(RotateTurret);
            }
        }

        private void OnTargetOutOfRange(EnemyView target)
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