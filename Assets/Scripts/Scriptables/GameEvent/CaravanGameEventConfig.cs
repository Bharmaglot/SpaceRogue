using SpaceRogue.Enums;
using UnityEngine;


namespace SpaceRogue.Scriptables.GameEvent
{
    [CreateAssetMenu(fileName = nameof(CaravanGameEventConfig), menuName = "Configs/GameEvent/" + nameof(CaravanGameEventConfig))]
    public sealed class CaravanGameEventConfig : BaseCaravanGameEventConfig
    {

        #region Properties

        [field: SerializeField, Header("Caravan Settings"), Min(0.0f)] public float AddHealth { get; private set; } = 50.0f;

        #endregion


        #region CodeLife

        public CaravanGameEventConfig()
        {
            GameEventType = GameEventType.Caravan;
        }

        #endregion

    }
}