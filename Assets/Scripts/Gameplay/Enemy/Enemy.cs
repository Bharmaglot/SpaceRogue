using System;
using Gameplay.Enemy.Behaviour;
using Gameplay.Movement;
using Gameplay.Shooting;
using Gameplay.Survival;
using SpaceRogue.Player.Movement;
using UnityEngine;


namespace Gameplay.Enemy
{
    public sealed class Enemy : IDisposable
    {
        private readonly UnitMovement _unitMovement;
        private readonly UnitTurning _unitTurning;
        private readonly UnitWeapon _unitWeapon;
        private readonly EnemyBehaviourSwitcher _behaviourSwitcher;

        public event Action<Enemy> EnemyDestroyed = _ => { };

        public EnemyView EnemyView { get; }
        public EntitySurvival Survival { get; }

        public Enemy(
            EnemyView enemyView, 
            UnitMovement unitMovement,
            UnitTurning unitTurning,
            UnitWeapon unitWeapon,
            EnemyBehaviourSwitcher behaviourSwitcher,
            EntitySurvival enemySurvival)
        {
            EnemyView = enemyView;
            _unitMovement = unitMovement;
            _unitTurning = unitTurning;
            _unitWeapon = unitWeapon;
            _behaviourSwitcher = behaviourSwitcher;
            Survival = enemySurvival;

            Survival.EntityHealth.HealthReachedZero += OnDeath;
        }

        public void Dispose()
        {
            Survival.EntityHealth.HealthReachedZero -= OnDeath;

            EnemyDestroyed.Invoke(this);

            Survival.Dispose();
            _unitMovement.Dispose();
            _unitTurning.Dispose();
            _unitWeapon.Dispose();
            _behaviourSwitcher.Dispose();

            UnityEngine.Object.Destroy(EnemyView.gameObject);
        }

        public void SetGroupDirection(Vector3 direction)
        {
            _behaviourSwitcher.CurrentBehaviour.SetMovementDirection(direction);
        }

        private void OnDeath()
        {
            Dispose();
        }
    }
}