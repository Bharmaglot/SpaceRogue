using SpaceRogue.Gameplay.GameProgress;
using System;


namespace SpaceRogue.Gameplay.Missions
{
    public sealed class KillEnemiesMission : BaseMission, IDisposable
    {

        #region Events

        public event Action<int> KillCountChanged;

        public override event Action Completed;

        #endregion


        #region Fields

        private readonly EnemyDeathObserver _enemyDeathObserver;
        private bool _completed;

        #endregion


        #region Properties

        public int EnemiesKilled { get; private set; }
        public int EnemiesToKill { get; private set; }

        #endregion


        #region CodeLife

        public KillEnemiesMission(int enemiesToKill, EnemyDeathObserver enemyDeathObserver)
        {
            _enemyDeathObserver = enemyDeathObserver;
            EnemiesToKill = enemiesToKill;
            EnemiesKilled = 0;

            _enemyDeathObserver.EnemyDestroyed += OnEnemyDestroyed;
        }

        public void Dispose() => _enemyDeathObserver.EnemyDestroyed -= OnEnemyDestroyed;

        #endregion

        #region Methods

        //TODO: Hard-code: Remove when unneeded
        public void CompleteInstantly()
        {
            EnemiesKilled = EnemiesToKill;
            KillCountChanged?.Invoke(EnemiesKilled);

            _completed = true;
            Completed?.Invoke();
        }

        private void OnEnemyDestroyed(global::Gameplay.Enemy.Enemy _) => IncreaseKillCount();

        private void IncreaseKillCount()
        {
            if (_completed)
            {
                return;
            }

            EnemiesKilled += 1;
            KillCountChanged?.Invoke(EnemiesKilled);

            if (EnemiesKilled >= EnemiesToKill)
            {
                _completed = true;
                Completed?.Invoke();
            }
        }

        #endregion

    }
}