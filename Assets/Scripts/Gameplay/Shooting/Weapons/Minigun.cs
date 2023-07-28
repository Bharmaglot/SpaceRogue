using Gameplay.Mechanics.Meter;
using Gameplay.Mechanics.Timer;
using SpaceRogue.Enums;
using SpaceRogue.Gameplay.Shooting.Factories;
using SpaceRogue.Gameplay.Shooting.Scriptables;
using System;
using UnityEngine;
using Utilities.Mathematics;


namespace SpaceRogue.Gameplay.Shooting.Weapons
{
    public sealed class Minigun : Weapon, IDisposable
    {

        #region Fields

        private readonly MinigunConfig _config;
        private readonly EntityType _entityType;
        private readonly ProjectileFactory _projectileFactory;
        private readonly MeterWithCooldown _overheatMeter;

        private float _currentSprayAngle;

        #endregion


        #region Properties

        private float SprayIncrease => (_config.MaxSprayAngle - _config.SprayAngle) / (_config.TimeToOverheat * (1 / _config.Cooldown));

        #endregion


        #region CodeLife

        public Minigun(
            MinigunConfig config,
            EntityType entityType,
            ProjectileFactory projectileFactory,
            TimerFactory timerFactory) : base(config, timerFactory)
        {
            _config = config;
            _entityType = entityType;
            _projectileFactory = projectileFactory;

            _overheatMeter = new MeterWithCooldown(0.0f, config.TimeToOverheat, config.OverheatCoolDown, timerFactory);
            _overheatMeter.OnCooldownEnd += ResetSpray;
            _currentSprayAngle = config.SprayAngle;
        }

        public new void Dispose()
        {
            _overheatMeter.OnCooldownEnd -= ResetSpray;
            _overheatMeter.Dispose();
            base.Dispose();
        }

        #endregion


        #region Methods

        public override void CommenceFiring(Vector2 bulletPosition, Quaternion turretRotation)
        {
            if (_overheatMeter.IsOnCooldown || IsOnCooldown)
            {
                return;
            }

            FireSingleProjectile(bulletPosition, turretRotation);
            AddHeat();

            CooldownTimer.Start();
        }

        private void ResetSpray() => _currentSprayAngle = _config.SprayAngle;

        private void FireSingleProjectile(Vector2 bulletPosition, Quaternion turretRotation)
        {
            var angle = _currentSprayAngle / 2;

            var pelletAngle = RandomPicker.PickRandomBetweenTwoValues(-angle, angle);
            var pelletDirection = turretRotation * Quaternion.AngleAxis(pelletAngle, Vector3.forward);

            _projectileFactory.Create(new ProjectileSpawnParams(bulletPosition, pelletDirection, _entityType, _config.MinigunProjectile, this));
        }

        private void AddHeat()
        {
            _overheatMeter.Fill(_config.Cooldown);
            IncreaseSpray();
        }

        private void IncreaseSpray()
        {
            if (_currentSprayAngle >= _config.MaxSprayAngle)
            {
                return;
            }

            _currentSprayAngle += SprayIncrease;
        }

        #endregion

    }
}