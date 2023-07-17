using System;
using System.Collections.Generic;
using Gameplay.Enemy;
using Zenject;

namespace Gameplay.GameProgress
{
    public class EnemyDeathObserver : IInitializable, IDisposable
    {
        private readonly EnemyFactory _enemyFactory;
        private readonly List<Enemy.Enemy> _enemies;

        public event Action<Enemy.Enemy> EnemyDestroyed;

        public EnemyDeathObserver(EnemyFactory enemyFactory)
        {
            _enemyFactory = enemyFactory;
            _enemies = new List<Enemy.Enemy>();
        }
        
        public void Initialize()
        {
            _enemyFactory.EnemyCreated += OnEnemyCreated;
        }

        public void Dispose()
        {
            _enemyFactory.EnemyCreated -= OnEnemyCreated;
            
            foreach (var enemy in _enemies)
            {
                enemy.EnemyDestroyed -= OnEnemyDestroyed;
            }
            _enemies.Clear();
        }

        private void OnEnemyCreated(Enemy.Enemy enemy)
        {
            enemy.EnemyDestroyed += OnEnemyDestroyed;
            _enemies.Add(enemy);
        }

        private void OnEnemyDestroyed(Enemy.Enemy enemy)
        {
            EnemyDestroyed?.Invoke(enemy);
            enemy.EnemyDestroyed -= OnEnemyDestroyed;
            _enemies.Remove(enemy);
        }
    }
}