using SpaceRogue.Enums;
using SpaceRogue.Gameplay.GameEvent.Scriptables;
using UnityEngine;


namespace SpaceRogue.Scriptables.GameEvent
{
    [CreateAssetMenu(fileName = nameof(CometGameEventConfig), menuName = "Configs/GameEvent/" + nameof(CometGameEventConfig))]
    public sealed class CometGameEventConfig : GameEventConfig
    {

        #region Properties

        [field: SerializeField, Header("Comet Settings")] public CometConfig CometConfig { get; private set; }

        #endregion


        #region CodeLife

        public CometGameEventConfig()
        {
            GameEventType = GameEventType.Comet;
        }

        #endregion

    }
}