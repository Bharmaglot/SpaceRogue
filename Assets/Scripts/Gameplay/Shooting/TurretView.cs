using SpaceRogue.Abstraction;
using System;
using UnityEngine;


namespace SpaceRogue.Gameplay.Shooting
{
    [RequireComponent(typeof(CircleCollider2D))]
    public sealed class TurretView : MonoBehaviour
    {

        #region Fields

        private const float ANGLE_CORRECTION = 270.0f;

        #endregion


        #region Events

        public event Action<EntityViewBase> TargetEntersTrigger = (_) => { };

        public event Action<EntityViewBase> TargetExitsTrigger = (_) => { };

        #endregion


        #region Mono

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

        #endregion


        #region Methods

        public void Rotate(Vector3 direction, float speed)
        {
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(angle + ANGLE_CORRECTION, Vector3.forward), speed);
        }

        #endregion

    }
}