using System;
using Gameplay.Enemy;
using UnityEngine;

namespace Gameplay.Services
{
    public sealed class EnemiesAlarm
    {
        public event Action<EnemyView, Transform, float> Alarm = (_, _, _) => { };

        public void AlarmSignal(EnemyView signalingEnemy, Transform targetTransform, float alarmRadius)
        {
            Alarm.Invoke(signalingEnemy, targetTransform, alarmRadius);
        }
    }
}