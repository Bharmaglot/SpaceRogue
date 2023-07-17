using Gameplay.Player;
using UnityEngine;
using Utilities.Reactive.SubscriptionProperty;

namespace Gameplay.Enemy.Behaviour
{
    public sealed class LegacyEnemyIdleBehaviour : LegacyEnemyBehaviour
    {
        public LegacyEnemyIdleBehaviour(SubscribedProperty<EnemyState> enemyState,
            EnemyView view,
            PlayerController playerController,
            EnemyBehaviourConfig config) : base(enemyState, view, playerController, config)
        {
        }

        protected override void OnUpdate() { }

        protected override void DetectPlayer()
        {
            if (Vector3.Distance(View.transform.position, PlayerView.transform.position) < Config.PlayerDetectionRadius)
            {
                EnterCombat();
            }
        }

        private void EnterCombat()
        {
            ChangeState(EnemyState.InCombat);
        }
    }
}