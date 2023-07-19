using Gameplay.GameProgress;
using Gameplay.Missions.Scriptables;
using UnityEngine;
using Zenject;

namespace Gameplay.Missions.Factories
{
    //TODO: Hard-code: Remove this factory when level missions can be different
    public sealed class KillMissionFactory : PlaceholderFactory<int, KillEnemiesMissionConfig, KillEnemiesMission>
    {
        private readonly EnemyDeathObserver _enemyDeathObserver;

        public KillMissionFactory(EnemyDeathObserver enemyDeathObserver)
        {
            _enemyDeathObserver = enemyDeathObserver;
        }
        
        public override KillEnemiesMission Create(int enemiesCount, KillEnemiesMissionConfig config)
        {
            var enemiesToWinCount = Mathf.Clamp(config.KillCount, 1, enemiesCount);
            return new KillEnemiesMission(enemiesToWinCount, _enemyDeathObserver);
        }
    }
}