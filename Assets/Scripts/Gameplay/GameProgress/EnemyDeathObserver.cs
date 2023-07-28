using Gameplay.Enemy;
using System;
using System.Collections.Generic;


namespace SpaceRogue.Gameplay.GameProgress
{
    public class EnemyDeathObserver : IDisposable
    {

        #region Events

        public event Action<global::Gameplay.Enemy.Enemy> EnemyDestroyed;

        #endregion


        #region Fields

        private readonly EnemyFactory _enemyFactory;
        private readonly List<global::Gameplay.Enemy.Enemy> _enemies;

        #endregion


        #region CodeLife

        public EnemyDeathObserver(EnemyFactory enemyFactory)
        {
            _enemyFactory = enemyFactory;
            _enemies = new List<global::Gameplay.Enemy.Enemy>();

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

        #endregion


        #region Methods

        private void OnEnemyCreated(global::Gameplay.Enemy.Enemy enemy)
        {
            enemy.EnemyDestroyed += OnEnemyDestroyed;
            _enemies.Add(enemy);
        }

        private void OnEnemyDestroyed(global::Gameplay.Enemy.Enemy enemy)
        {
            EnemyDestroyed?.Invoke(enemy);
            enemy.EnemyDestroyed -= OnEnemyDestroyed;
            _enemies.Remove(enemy);
        }

        #endregion

    }
}