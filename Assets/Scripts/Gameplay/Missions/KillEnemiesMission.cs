using System;
using Gameplay.GameProgress;
using Zenject;

namespace Gameplay.Missions
{
    public sealed class KillEnemiesMission : BaseMission, IInitializable, IDisposable
    {
        private readonly EnemyDeathObserver _enemyDeathObserver;
        private bool _completed;
        
        public event Action<int> KillCountChanged;
        public override event Action Completed;
        
        public int EnemiesKilled { get; private set; }
        public int EnemiesToKill { get; private set; }

        public KillEnemiesMission(int enemiesToKill, EnemyDeathObserver enemyDeathObserver)
        {
            _enemyDeathObserver = enemyDeathObserver;
            EnemiesToKill = enemiesToKill;
            EnemiesKilled = 0;
        }
        
        public void Initialize()
        {
            _enemyDeathObserver.EnemyDestroyed += OnEnemyDestroyed;
        }

        public void Dispose()
        {
            _enemyDeathObserver.EnemyDestroyed -= OnEnemyDestroyed;
        }

        //TODO: Hard-code: Remove when unneeded
        public void CompleteInstantly()
        {
            EnemiesKilled = EnemiesToKill;
            KillCountChanged?.Invoke(EnemiesKilled);
            
            _completed = true;
            Completed?.Invoke();
        }

        private void OnEnemyDestroyed(Enemy.Enemy _)
        {
            IncreaseKillCount();
        }

        private void IncreaseKillCount()
        {
            if (_completed) return;
            
            EnemiesKilled += 1;
            KillCountChanged?.Invoke(EnemiesKilled);
            
            if (EnemiesKilled >= EnemiesToKill)
            {
                _completed = true;
                Completed?.Invoke();
            }
        }
    }
}