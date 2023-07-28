using SpaceRogue.Gameplay.GameProgress;
using SpaceRogue.Gameplay.Missions.Scriptables;
using System;
using Zenject;


namespace SpaceRogue.Gameplay.Missions.Factories
{
    public sealed class MissionFactory : IFactory<BaseMissionConfig, BaseMission>
    {

        #region Fields

        private readonly EnemyDeathObserver _enemyDeathObserver;

        #endregion


        #region CodeLife

        public MissionFactory(EnemyDeathObserver enemyDeathObserver)
        {
            _enemyDeathObserver = enemyDeathObserver;
        }

        #endregion


        #region Methods

        public BaseMission Create(BaseMissionConfig config) => config.Type switch
        {
            MissionType.None => new KillEnemiesMission(1, _enemyDeathObserver),
            MissionType.KillEnemies => new KillEnemiesMission((config as KillEnemiesMissionConfig)!.KillCount, _enemyDeathObserver),
            _ => throw new ArgumentOutOfRangeException()
        };

        #endregion

    }
}