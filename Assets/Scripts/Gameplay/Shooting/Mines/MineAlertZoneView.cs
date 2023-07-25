using SpaceRogue.Abstraction;
using System;
using UnityEngine;

namespace SpaceRogue.Shooting
{
    public sealed class MineAlertZoneView : MonoBehaviour
    {

        #region Events

        public event Action<EntityViewBase> TargetEnterAlarmZone;

        #endregion


        #region Methods

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out EntityViewBase target))
            {
                TargetEnterAlarmZone?.Invoke(target);
            }
        }

        #endregion

    }
}
