using UnityEngine;


namespace SpaceRogue.Gameplay.Shooting.Scriptables
{
    [CreateAssetMenu(fileName = nameof(TurretConfig), menuName = "Configs/Weapons/" + nameof(TurretConfig))]
    public sealed class TurretConfig : ScriptableObject
    {
        [field: SerializeField] public float Range { get; private set; }
        [field: SerializeField] public float TurningSpeed { get; private set; }
    }
}