using SpaceRogue.Enums;
using UnityEngine;


namespace SpaceRogue.Scriptables.GameEvent
{
    [CreateAssetMenu(fileName = nameof(CaravanTrapGameEventConfig), menuName = "Configs/GameEvent/" + nameof(CaravanTrapGameEventConfig))]
    public sealed class CaravanTrapGameEventConfig : BaseCaravanGameEventConfig
    {

        #region Properties

        [field: SerializeField, Header("CaravanTrap Settings"), Min(0.0f)] public float AlertRadius { get; private set; } = 70.0f;

        #endregion


        #region CodeLife

        public CaravanTrapGameEventConfig()
        {
            GameEventType = GameEventType.CaravanTrap;
        }

        #endregion

    }
}