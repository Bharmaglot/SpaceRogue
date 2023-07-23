using SpaceRogue.Abstraction;
using System;
using UnityEngine;


namespace SpaceRogue.Gameplay.Space.Obstacle
{
    [RequireComponent(typeof(Collider2D))]
    public sealed class SpaceObstacleView : MonoBehaviour
    {
        #region Events

        public event Action<EntityViewBase> OnTriggerEnter;

        public event Action<EntityViewBase> OnTriggerStay;

        public event Action<EntityViewBase> OnTriggerExit;

        #endregion

        #region Mono

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out EntityViewBase unitView))
            {
                OnTriggerEnter?.Invoke(unitView);
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out EntityViewBase unitView))
            {
                OnTriggerStay?.Invoke(unitView);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out EntityViewBase unitView))
            {
                OnTriggerExit?.Invoke(unitView);
            }
        }

        #endregion
    }
}