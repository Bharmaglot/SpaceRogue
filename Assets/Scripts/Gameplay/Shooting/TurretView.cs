using SpaceRogue.Abstraction;
using System;
using UnityEngine;


namespace Gameplay.Shooting
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class TurretView : MonoBehaviour
    {
        public event Action<EntityViewBase> TargetEntersTrigger = (_) => { };
        public event Action<EntityViewBase> TargetExitsTrigger = (_) => { };

        private void OnTriggerEnter2D(Collider2D collision)
        {

            if (collision.TryGetComponent(out EntityViewBase target))
            {
                TargetEntersTrigger(target);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out EntityViewBase target))
            {
                TargetExitsTrigger(target);
            }
        }

        internal void Rotate(Vector3 direction, float speed)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(angle + 270f, Vector3.forward), speed);
        }
    }
}
