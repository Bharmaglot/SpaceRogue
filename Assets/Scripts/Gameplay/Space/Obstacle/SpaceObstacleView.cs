using System;
using UnityEngine;
using SpaceRogue.Abstraction;


namespace Gameplay.Space.Obstacle
{
    [RequireComponent(typeof(Collider2D))]
    public sealed class SpaceObstacleView : MonoBehaviour
    {
        public event Action<EntityViewBase> OnTriggerEnter = (EntityViewBase _) => {};
        public event Action<EntityViewBase> OnTriggerStay = (EntityViewBase _) => { };
        public event Action<EntityViewBase> OnTriggerExit = (EntityViewBase _) => { };

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out EntityViewBase unitView))
            {
                OnTriggerEnter(unitView);
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if(collision.gameObject.TryGetComponent(out EntityViewBase unitView))
            {
                OnTriggerStay(unitView);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out EntityViewBase unitView))
            {
                OnTriggerExit(unitView);
            }
        }
    }
}