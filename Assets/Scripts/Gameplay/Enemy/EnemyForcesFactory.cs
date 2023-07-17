using Gameplay.Enemy.Scriptables;
using Gameplay.Space.Generator;
using Zenject;

namespace Gameplay.Enemy
{
    public sealed class EnemyForcesFactory : PlaceholderFactory<int, SpawnPointsFinder, EnemyForces>
    {
        private readonly EnemySpawnConfig _enemySpawnConfig;
        private readonly EnemiesGroupFactory _enemiesGroupFactory;

        public EnemyForcesFactory(EnemySpawnConfig enemySpawnConfig, EnemiesGroupFactory enemiesGroupFactory)
        {
            _enemySpawnConfig = enemySpawnConfig;
            _enemiesGroupFactory = enemiesGroupFactory;
        }

        public override EnemyForces Create(int enemyGroupCount, SpawnPointsFinder spawnPointsFinder)
        {
            return new(enemyGroupCount, _enemySpawnConfig, spawnPointsFinder, _enemiesGroupFactory);
        }
    }
}