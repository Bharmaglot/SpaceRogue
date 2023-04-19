using Utilities.Mathematics;
using Gameplay.Abstracts;
using System;
using Gameplay.Shooting.Factories;
using Gameplay.Shooting.Scriptables;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Services;

namespace Gameplay.Shooting
{
    public sealed class TurretMountedWeapon : MountedWeapon, IDisposable
    {
        private readonly TurretView _turretView;
        private readonly Transform _gunPointViewTransform;
        private Transform _currentTarget;
        private List<Transform> _targets = new List<Transform>();

        private readonly float _rotationSpeed;
        private readonly Updater _updater;

        public TurretMountedWeapon(Weapon weapon, EntityView entityView, TurretViewFactory turretViewFactory, GunPointViewFactory gunPointViewFactory, TurretConfig config, Updater updater) : base(weapon, entityView)
        {
            _updater = updater;
            var unitScale = UnitViewTransform.localScale;
            var gunPointPosition = UnitViewTransform.position + UnitViewTransform.TransformDirection(0.6f * Mathf.Max(unitScale.x, unitScale.y) * Vector3.up);
            var turretView = turretViewFactory.Create(UnitViewTransform, config);
            var gunPoint = gunPointViewFactory.Create(gunPointPosition, UnitViewTransform.rotation, _turretView.transform);
            _gunPointViewTransform = gunPoint.transform;


            _rotationSpeed = config.TurnigSpeed;
            _turretView = turretView;
            _turretView.OnTriggerEnterTarget += TargetOnCollision;
            _turretView.OnTriggerExitTarget += TargetOutCollision;
        }

        public override void CommenceFiring()
        {
            Weapon.CommenceFiring(_gunPointViewTransform.position, _gunPointViewTransform.rotation);
        }

        private void RotateTurret()
        {
            if (_currentTarget != null)
            {
                var lookPos = _currentTarget.transform.position - _turretView.transform.position;
                float _angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
                _turretView.transform.rotation = Quaternion.Slerp(_turretView.transform.rotation, Quaternion.AngleAxis(_angle, Vector3.forward), _rotationSpeed * Time.deltaTime);
            }
        }

        private Transform PickNewTarget()
        {
            if (!_targets.Any()) return null;
            return _targets.MinBy(t => (_turretView.transform.position - t.transform.position).sqrMagnitude);
        }

        private void TargetOnCollision(Transform target)
        {
            _targets.Add(target);
            if (_currentTarget is null)
            {
                _currentTarget = target;
                _updater.SubscribeToUpdate(RotateTurret);
            }
        }

        private void TargetOutCollision(Transform target)
        {
            _targets.Remove(target);
            if (_currentTarget == target)
            {
                _currentTarget = PickNewTarget();
                if (!_currentTarget) _updater.UnsubscribeFromUpdate(RotateTurret);
            }
        }

        public void Dispose()
        {
            _updater.UnsubscribeFromUpdate(RotateTurret);
            _turretView.OnTriggerEnterTarget -= TargetOnCollision;
            _turretView.OnTriggerExitTarget -= TargetOutCollision;
        }
    }
}