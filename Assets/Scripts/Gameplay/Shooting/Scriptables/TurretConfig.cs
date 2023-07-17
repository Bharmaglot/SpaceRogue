using UnityEngine;

namespace Gameplay.Shooting.Scriptables
{
    [CreateAssetMenu(fileName = nameof(TurretConfig), menuName = "Configs/Weapons/" + nameof(TurretConfig))]
    public class TurretConfig : ScriptableObject
    {
        [field: SerializeField] public float Range { get; private set; }
        [field: SerializeField] public float TurningSpeed { get; private set; }
    }
}