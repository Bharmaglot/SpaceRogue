using Gameplay.Enemy.Scriptables;
using Gameplay.Mechanics.Timer;
using SpaceRogue.Services;
using UnityEngine;
using Zenject;


namespace Gameplay.Enemy
{
    public sealed class EnemiesGroupFactory : PlaceholderFactory<EnemyGroupConfig, Vector2, EnemiesGroup>
    {
        private readonly Updater _updater;
        private readonly TimerFactory _timerFactory;
        private readonly EnemyFactory _enemyFactory;

        public EnemiesGroupFactory(Updater updater, TimerFactory timerFactory, EnemyFactory enemyFactory)
        {
            _updater = updater;
            _timerFactory = timerFactory;
            _enemyFactory = enemyFactory;
        }

        public override EnemiesGroup Create(EnemyGroupConfig enemyGroupConfig, Vector2 spawnPoint)
        {
            var timer = _timerFactory.Create(enemyGroupConfig.TimeToPickNewDirectionInSeconds);
            return new(_updater, timer, enemyGroupConfig, spawnPoint, _enemyFactory);
        }
    }
}