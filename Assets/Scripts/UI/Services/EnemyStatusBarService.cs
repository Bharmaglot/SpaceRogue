using System;
using System.Collections.Generic;
using System.Linq;
using UI.Game;
using Gameplay.Enemy;
using UnityEngine;

namespace UI.Services
{
    public sealed class EnemyStatusBarService : IDisposable
    {
        private readonly EnemyHealthBarsView _barsView;
        private readonly EnemyFactory _enemyFactory;
        private readonly EnemyStatusBarViewFactory _statusBarViewFactory;
        private readonly FloatStatusBarFactory _floatStatusBarFactory;

        private Dictionary<Enemy, FloatStatusBar> _viewDictionary = new();

        public EnemyStatusBarService(
            EnemyHealthBarsView barsView, 
            EnemyFactory enemyFactory, 
            EnemyStatusBarViewFactory statusBarViewFactory,
            FloatStatusBarFactory floatStatusBarFactory)
        {
            _barsView = barsView;
            _enemyFactory = enemyFactory;
            _statusBarViewFactory = statusBarViewFactory;
            _floatStatusBarFactory = floatStatusBarFactory;
            
            _enemyFactory.EnemyCreated += OnEnemyCreated;
        }

        public void Dispose()
        {
            _enemyFactory.EnemyCreated -= OnEnemyCreated;
            var statusBars = _viewDictionary.Values.ToArray();
            foreach (var statusBar in statusBars)
            {
                if (statusBar is not null)
                {
                    statusBar.Dispose();
                }
            }
            _viewDictionary.Clear();
        }

        private void OnEnemyCreated(Enemy enemy)
        {
            var statusBarView = _statusBarViewFactory.Create(enemy.Survival, _barsView);

            if (enemy.EnemyView.TryGetComponent(out Collider2D collider))
            {
                var statusBar = _floatStatusBarFactory.Create(statusBarView, collider, enemy.Survival);
                _viewDictionary[enemy] = statusBar;
                enemy.EnemyDisposed += OnEnemyDisposed;
            }
        }

        private void OnEnemyDisposed(Enemy enemy)
        {
            enemy.EnemyDisposed -= OnEnemyDisposed;
            var statusBar = _viewDictionary[enemy];
            statusBar.Dispose();
            _viewDictionary.Remove(enemy);
        }
    }
}