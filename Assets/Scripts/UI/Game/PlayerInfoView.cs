using UnityEngine;

namespace UI.Game
{
    public sealed class PlayerInfoView : MonoBehaviour
    {
        [field: SerializeField] public PlayerStatusBarView PlayerStatusBarView { get; private set; }
        [field: SerializeField] public PlayerSpeedometerView PlayerSpeedometerView { get; private set; }
        [field: SerializeField] public PlayerUsedItemView PlayerWeaponView { get; private set; }
        [field: SerializeField] public PlayerUsedItemView PlayerAbilityView { get; private set; }
        [field: SerializeField] public CharacterView CharacterView { get; private set; }
    }
}