using UnityEngine;


namespace SpaceRogue.Shooting
{
    public class MineView : MonoBehaviour
    {

        #region Fields

        [field: SerializeField] public Transform MineBodyTransform { get; private set; }
        [field: SerializeField] public Transform MineAlertZoneTansform { get; private set; }
        [field: SerializeField] public Transform MineTimerVisualTransform { get; private set; }
        [field: SerializeField] public Transform ExplozionTransform { get; private set; }

        [field: SerializeField] public MineAlertZoneView MineAlertZoneView { get; private set; }
        [field: SerializeField] public MineExploseView MineExploseView { get; private set; }

        #endregion

    }
}
