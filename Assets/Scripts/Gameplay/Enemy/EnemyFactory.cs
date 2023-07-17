using Gameplay.Enemy.Behaviour;
using Gameplay.Enemy.Movement;
using Gameplay.Enemy.Scriptables;
using Gameplay.Movement;
using Gameplay.Shooting;
using Gameplay.Survival;
using System;
using UnityEngine;
using Zenject;

namespace Gameplay.Enemy
{
    public sealed class EnemyFactory : PlaceholderFactory<Vector2, EnemyConfig, Enemy>
    {
        private readonly EnemyViewFactory _enemyViewFactory;
        private readonly EnemyInputFactory _enemyInputFactory;
        private readonly UnitMovementModelFactory _unitMovementModelFactory;
        private readonly UnitMovementFactory _unitMovementFactory;
        private readonly UnitTurningFactory _unitTurningFactory;
        private readonly UnitWeaponFactory _unitWeaponFactory;
        private readonly EnemyBehaviourSwitcherFactory _enemyBehaviourSwitcherFactory;
        private readonly EntitySurvivalFactory _entitySurvivalFactory;

        public event Action<Enemy> EnemyCreated = _ => { };

        public EnemyFactory(
            EnemyViewFactory enemyViewFactory, 
            EnemyInputFactory enemyInputFactory,
            UnitMovementModelFactory unitMovementModelFactory,
            UnitMovementFactory unitMovementFactory,
            UnitTurningFactory unitTurningFactory,
            UnitWeaponFactory unitWeaponFactory,
            EnemyBehaviourSwitcherFactory enemyBehaviourSwitcherFactory,
            EntitySurvivalFactory entitySurvivalFactory)
        {
            _enemyViewFactory = enemyViewFactory;
            _enemyInputFactory = enemyInputFactory;
            _unitMovementModelFactory = unitMovementModelFactory;
            _unitMovementFactory = unitMovementFactory;
            _unitTurningFactory = unitTurningFactory;
            _unitWeaponFactory = unitWeaponFactory;
            _enemyBehaviourSwitcherFactory = enemyBehaviourSwitcherFactory;
            _entitySurvivalFactory = entitySurvivalFactory;
        }

        public override Enemy Create(Vector2 spawnPoint, EnemyConfig enemyConfig)
        {
            var enemyView = _enemyViewFactory.Create(spawnPoint, enemyConfig);
            var enemyInput = _enemyInputFactory.Create();
            var model = _unitMovementModelFactory.Create(enemyConfig.Movement);
            var unitMovement = _unitMovementFactory.Create(enemyView, enemyInput, model);
            var unitTurning = _unitTurningFactory.Create(enemyView, enemyInput, model);
            var unitWeapon = _unitWeaponFactory.Create(enemyView, enemyConfig.MountedWeapon, enemyInput);
            var enemyBehaviourSwitcher = _enemyBehaviourSwitcherFactory.Create(
                enemyView,
                enemyInput,
                model,
                enemyConfig.Behaviour);
            var enemySurvival = _entitySurvivalFactory.Create(enemyView, enemyConfig.Survival);
            
            var enemy = new Enemy(
                enemyView,
                unitMovement,
                unitTurning,
                unitWeapon,
                enemyBehaviourSwitcher,
                enemySurvival);
            EnemyCreated.Invoke(enemy);
            return enemy;
        }
    }
}