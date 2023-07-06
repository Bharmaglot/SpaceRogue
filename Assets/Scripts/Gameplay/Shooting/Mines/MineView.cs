using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Shooting
{
    public class MineView : MonoBehaviour
    {
        [SerializeField] internal Transform MineBodyTransform;
        [SerializeField] internal Transform MineAlertZoneTansform;
        [SerializeField] internal Transform MineTimerVisualTransform;
        [SerializeField] internal Transform ExplozionTransform;

        [SerializeField] internal MineAlertZoneView MineAlertZoneView;
        [SerializeField] internal MineExploseView MineExploseView;
    }
}
