using SpaceRogue.Abstraction;
using System;
using UnityEngine;

public class MineExploseView : MonoBehaviour
{
    public event Action<EntityViewBase> TargetEnterExploseZone = (_) => { };

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.TryGetComponent(out EntityViewBase target))
        {
            TargetEnterExploseZone(target);
        }
    }
}