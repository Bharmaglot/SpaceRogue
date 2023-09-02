using SpaceRogue.Abstraction;
using System;
using UnityEngine;

namespace SpaceRogue.Gameplay.Shooting
{
    public class ReactiveObjectView : MonoBehaviour
    {

        #region Fields

        [SerializeField] public TargetFinderView TargetFinderView;

        #endregion


        #region Events

        public event Action TargetEntersTrigger;

        #endregion


        #region Methods

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out EntityViewBase target))
            {
                TargetEntersTrigger?.Invoke();
            }
        }

        #endregion

    }
}