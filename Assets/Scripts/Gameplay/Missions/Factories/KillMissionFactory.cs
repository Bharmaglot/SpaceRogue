using SpaceRogue.Gameplay.GameProgress;
using SpaceRogue.Gameplay.Missions.Scriptables;
using UnityEngine;
using Zenject;


namespace SpaceRogue.Gameplay.Missions.Factories
{
    //TODO: Hard-code: Remove this factory when level missions can be different
    public sealed class KillMissionFactory : PlaceholderFactory<int, KillEnemiesMissionConfig, KillEnemiesMission>
    {

        #region Fields

        private readonly EnemyDeathObserver _enemyDeathObserver;

        #endregion


        #region CodeLife

        public KillMissionFactory(EnemyDeathObserver enemyDeathObserver)
        {
            _enemyDeathObserver = enemyDeathObserver;
        }

        #endregion


        #region Methods

        public override KillEnemiesMission Create(int enemiesCount, KillEnemiesMissionConfig config)
        {
            var enemiesToWinCount = Mathf.Clamp(config.KillCount, 1, enemiesCount);
            return new KillEnemiesMission(enemiesToWinCount, _enemyDeathObserver);
        }

        #endregion

    }
}