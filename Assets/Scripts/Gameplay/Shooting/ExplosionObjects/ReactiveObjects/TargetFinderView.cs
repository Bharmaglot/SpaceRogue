using SpaceRogue.Abstraction;
using System;
using UnityEngine;


namespace SpaceRogue.Gameplay.Shooting
{
    public class TargetFinderView : MonoBehaviour
    {

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

    }
}
