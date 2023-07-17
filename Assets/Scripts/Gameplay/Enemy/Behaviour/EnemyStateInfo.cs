using System;

namespace Gameplay.Enemy.Behaviour
{
    public sealed class EnemyStateInfo
    {
        public EnemyState CurrentState { get; private set; }
        public EnemyState LastState { get; private set; }
        public Action<EnemyState> StateChanged { get; private set; }

        public EnemyStateInfo(
            EnemyState currentState,
            EnemyState lastState,
            Action<EnemyState> stateChanged)
        {
            CurrentState = currentState;
            LastState = lastState;
            StateChanged = stateChanged;
        }
    }
}