using Gameplay.Enemy.Scriptables;
using Gameplay.Space.Generator;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utilities.Mathematics;

namespace Gameplay.Enemy
{
    public sealed class EnemyForces : IDisposable
    {
        public List<EnemiesGroup> EnemiesGroups { get; private set; } = new();

        public EnemyForces(
            int enemyGroupCount,
            EnemySpawnConfig enemySpawnConfig,
            SpawnPointsFinder spawnPointsFinder,
            EnemiesGroupFactory enemiesGroupFactory)
        {
            for (int i = 0; i < enemyGroupCount; i++)
            {
                var groupConfig = RandomPicker.PickOneElementByWeights(enemySpawnConfig.Groups);
                var enemyCount = groupConfig.Squads.Sum(x => x.EnemyCount);

                if (!spawnPointsFinder.TryGetEnemySpawnPoint(enemyCount, out var spawnPoint))
                {
                    Debug.Log("EnemiesGroup spawn point not found");
                    continue;
                }

                var enemiesGroup = enemiesGroupFactory.Create(groupConfig, spawnPoint);
                enemiesGroup.EnemiesGroupDestroyed += OnGroupDestroyed;
                EnemiesGroups.Add(enemiesGroup);
            }
        }

        public void Dispose()
        {
            EnemiesGroups.Clear();
        }

        public int GetEnemiesCount()
        {
            var count = 0;

            foreach (var enemiesGroup in EnemiesGroups)
            {
                count += enemiesGroup.Enemies.Count; 
            }

            return count;
        }

        private void OnGroupDestroyed(EnemiesGroup enemiesGroup)
        {
            enemiesGroup.EnemiesGroupDestroyed -= OnGroupDestroyed;
            EnemiesGroups.Remove(enemiesGroup);

            if (EnemiesGroups.Count == 0) Dispose();
        }
    }
}