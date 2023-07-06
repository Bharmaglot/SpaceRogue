using SpaceRogue.Abstraction;
using System;
using UnityEngine;

public class MineAlertZoneView : MonoBehaviour
{
    public event Action<EntityViewBase> TargetEnterAlarmZone = (_) => { };

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.TryGetComponent(out EntityViewBase target))
        {
            TargetEnterAlarmZone(target);
        }
    }
}
