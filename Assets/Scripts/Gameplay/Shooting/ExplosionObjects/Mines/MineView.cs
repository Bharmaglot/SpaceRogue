using UnityEngine;


namespace SpaceRogue.Gameplay.Shooting
{
    public sealed class MineView : BaseExplosionObjectView
    {

        #region Properties

        [field: SerializeField] public Transform MineBodyTransform { get; private set; }
        [field: SerializeField] public Transform AlertZoneTransform { get; private set; }
        [field: SerializeField] public Transform TimerVisualTransform { get; private set; }
        [field: SerializeField] public MineAlertZoneView MineAlertZoneView { get; private set; }

        #endregion

    }
}
