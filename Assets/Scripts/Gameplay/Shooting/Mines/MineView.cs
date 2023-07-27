using UnityEngine;


namespace SpaceRogue.Gameplay.Shooting
{
    public sealed class MineView : MonoBehaviour
    {

        #region Properties

        [field: SerializeField] public Transform MineBodyTransform { get; private set; }
        [field: SerializeField] public Transform MineAlertZoneTransform { get; private set; }
        [field: SerializeField] public Transform MineTimerVisualTransform { get; private set; }
        [field: SerializeField] public Transform ExplosionTransform { get; private set; }

        [field: SerializeField] public MineAlertZoneView MineAlertZoneView { get; private set; }
        [field: SerializeField] public MineExplosionView MineExplosionView { get; private set; }

        #endregion

    }
}
