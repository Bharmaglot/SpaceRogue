using Gameplay.Mechanics.Timer;
using SpaceRogue.Abstraction;
using SpaceRogue.Enums;
using SpaceRogue.Gameplay.Abilities.Scriptables;
using SpaceRogue.Player.Movement;
using System.Collections.Generic;
using UnityEngine;


namespace SpaceRogue.Gameplay.Abilities
{
    public sealed class MinigunAbility : Ability
    {

        #region Fields

        private const string ENEMY_LAYER = "Enemy";
        private const string ENEMY_PROJECTILE_LAYER = "EnemyProjectile";
        private const string PLAYER_LAYER = "Player";
        private const string PLAYER_PROJECTILE_LAYER = "PlayerProjectile";

        private readonly MinigunAbilityConfig _minigunAbilityConfig;
        private readonly EntityViewBase _entityView;
        private readonly UnitMovement _unitMovement;

        private readonly Timer _accelerationTimer;
        private readonly Timer _damageAndCollisionIgnoreTimer;

        private readonly int _unitLayer;

        private readonly List<int> _ignoreLayers = new();

        #endregion


        #region CodeLife

        public MinigunAbility(
            MinigunAbilityConfig minigunAbilityConfig,
            EntityViewBase entityView,
            UnitMovement unitMovement,
            TimerFactory timerFactory) : base(minigunAbilityConfig, timerFactory)
        {
            _minigunAbilityConfig = minigunAbilityConfig;
            _entityView = entityView;
            _unitMovement = unitMovement;
            _accelerationTimer = timerFactory.Create(_minigunAbilityConfig.AccelerationDuration);
            _damageAndCollisionIgnoreTimer = timerFactory.Create(_minigunAbilityConfig.DamageAndCollisionIgnoreDuration);

            _unitLayer = _entityView.gameObject.layer;
            GetIgnoreLayers(_entityView.EntityType);

            _accelerationTimer.OnStart += AccelerationBoost;
            _accelerationTimer.OnExpire += AccelerationBoostStopped;
            _damageAndCollisionIgnoreTimer.OnStart += DamageAndCollisionIgnore;
            _damageAndCollisionIgnoreTimer.OnExpire += DamageAndCollisionIgnoreStopped;
        }

        #endregion


        #region Methods

        public override void UseAbility()
        {
            if (IsOnCooldown)
            {
                return;
            }

            _accelerationTimer.Start();
            _damageAndCollisionIgnoreTimer.Start();

            CooldownTimer.Start();
        }

        protected override void OnDispose()
        {
            DamageAndCollisionIgnoreStopped();
            _ignoreLayers.Clear();

            _accelerationTimer.OnStart -= AccelerationBoost;
            _accelerationTimer.OnExpire -= AccelerationBoostStopped;
            _damageAndCollisionIgnoreTimer.OnStart -= DamageAndCollisionIgnore;
            _damageAndCollisionIgnoreTimer.OnExpire -= DamageAndCollisionIgnoreStopped;

            _accelerationTimer.Dispose();
            _damageAndCollisionIgnoreTimer.Dispose();
        }

        private void GetIgnoreLayers(EntityType entityType)
        {
            if (entityType == EntityType.Player)
            {
                _ignoreLayers.Add(LayerMask.NameToLayer(ENEMY_LAYER));
                _ignoreLayers.Add(LayerMask.NameToLayer(ENEMY_PROJECTILE_LAYER));
                return;
            }

            if (entityType == EntityType.Enemy)
            {
                _ignoreLayers.Add(LayerMask.NameToLayer(PLAYER_LAYER));
                _ignoreLayers.Add(LayerMask.NameToLayer(PLAYER_PROJECTILE_LAYER));
                return;
            }
        }

        private void AccelerationBoost() => _unitMovement.ExtraSpeed = _minigunAbilityConfig.AccelerationBoost;

        private void AccelerationBoostStopped() => _unitMovement.ExtraSpeed = 0.0f;

        private void DamageAndCollisionIgnore()
        {
            foreach (var layer in _ignoreLayers)
            {
                Physics2D.IgnoreLayerCollision(_unitLayer, layer, true);
            }
        }

        private void DamageAndCollisionIgnoreStopped()
        {
            foreach (var layer in _ignoreLayers)
            {
                Physics2D.IgnoreLayerCollision(_unitLayer, layer, false);
            }
        }

        #endregion

    }
}