using Gameplay.Enemy;
using UnityEngine;
using System;

namespace Gameplay.Shooting
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class TurretView : MonoBehaviour
    {
        public event Action<Transform> OnTriggerEnterTarget = (_) => { };
        public event Action<Transform> OnTriggerExitTarget = (_) => { };

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<EnemyView>(out EnemyView target))
            {
                OnTriggerEnterTarget(target.transform);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent<EnemyView>(out EnemyView target))
            {
                OnTriggerExitTarget(target.transform);
            }
        }
    }
}