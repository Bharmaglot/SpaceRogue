using System;
using Gameplay.GameProgress;
using Gameplay.Missions.Scriptables;
using Zenject;

namespace Gameplay.Missions.Factories
{
    public sealed class MissionFactory : IFactory<BaseMissionConfig, BaseMission>
    {
        private readonly EnemyDeathObserver _enemyDeathObserver;

        public MissionFactory(EnemyDeathObserver enemyDeathObserver)
        {
            _enemyDeathObserver = enemyDeathObserver;
        }
        
        public BaseMission Create(BaseMissionConfig config)
        {
            return config.Type switch
            {
                MissionType.None => new KillEnemiesMission(1, _enemyDeathObserver),
                MissionType.KillEnemies => new KillEnemiesMission((config as KillEnemiesMissionConfig)!.KillCount, _enemyDeathObserver),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}