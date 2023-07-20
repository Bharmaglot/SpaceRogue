using Gameplay.Enemy.Scriptables;
using Gameplay.Mechanics.Timer;
using SpaceRogue.Services;
using System;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Mathematics;
using Utilities.Unity;
using Random = UnityEngine.Random;


namespace Gameplay.Enemy
{
    public sealed class EnemiesGroup : IDisposable
    {
        private readonly Updater _updater;
        private readonly Timer _timer;

        public List<Enemy> Enemies { get; } = new();

        public event Action<EnemiesGroup> EnemiesGroupDestroyed;

        public EnemiesGroup(
            Updater updater,
            Timer timer,
            EnemyGroupConfig groupConfig,
            Vector2 spawnPoint,
            EnemyFactory enemyFactory)
        {
            _updater = updater;
            _timer = timer;

            foreach (var squadConfig in groupConfig.Squads)
            {
                for (int j = 0; j < squadConfig.EnemyCount; j++)
                {
                    var enemyConfig = RandomPicker.PickOneElementByWeights(squadConfig.EnemyTypes);
                    var unitSize = enemyConfig.Prefab.transform.localScale;
                    var spawnCircleRadius = squadConfig.EnemyCount * 2;

                    var unitSpawnPoint = UnityHelper.GetEmptySpawnPoint(spawnPoint, unitSize, spawnCircleRadius);

                    var enemy = enemyFactory.Create(unitSpawnPoint, enemyConfig);
                    enemy.EnemyDisposed += OnDeath;
                    Enemies.Add(enemy);
                }
            }

            _updater.SubscribeToUpdate(PickRandomDirection);
        }

        public void Dispose()
        {
            var enemies = Enemies.ToArray();
            foreach (var enemy in enemies)
            {
                enemy?.Dispose();
            }
            Enemies.Clear();
            
            _timer.Dispose();
            _updater.UnsubscribeFromUpdate(PickRandomDirection);
            
            EnemiesGroupDestroyed?.Invoke(this);
        }
        
        private void OnDeath(Enemy enemy)
        {
            enemy.EnemyDisposed -= OnDeath;
            Enemies.Remove(enemy);

            if (Enemies.Count == 0) Dispose();
        }

        private void PickRandomDirection()
        {
            if (_timer.InProgress) return;

            var direction = Random.insideUnitCircle;

            foreach (var enemy in Enemies)
            {
                enemy?.SetGroupDirection(direction);
            }

            _timer.Start();
        }
    }
}