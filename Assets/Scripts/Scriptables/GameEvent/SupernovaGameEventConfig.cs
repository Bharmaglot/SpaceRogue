using SpaceRogue.Enums;
using UnityEngine;


namespace SpaceRogue.Scriptables.GameEvent
{
    [CreateAssetMenu(fileName = nameof(SupernovaGameEventConfig), menuName = "Configs/GameEvent/" + nameof(SupernovaGameEventConfig))]
    public sealed class SupernovaGameEventConfig : GameEventConfig
    {

        #region Properties

        [field: SerializeField, Header("Supernova Settings"), Min(0.0f)] public float SearchRadius { get; private set; } = 100.0f;
        [field: SerializeField, Tooltip("Seconds"), Min(0.1f)] public float TimeToExplosion { get; private set; } = 5.0f;
        [field: SerializeField] public Color WarningColor { get; private set; } = Color.red;
        [field: SerializeField] public Sprite SupernovaSprite { get; private set; }
        [field: SerializeField] public Color ShockwaveColor { get; private set; } = Color.red;
        [field: SerializeField, Min(0.0f)] public float ShockwaveSpeed { get; private set; } = 1.0f;
        [field: SerializeField, Min(0.0f)] public float ShockwaveRadius { get; private set; } = 100.0f;
        [field: SerializeField, Min(0.0f)] public float ShockwaveForce { get; private set; } = 50.0f;
        [field: SerializeField, Min(0.0f)] public float ShockwaveDamage { get; private set; } = 1.0f;

        #endregion


        #region CodeLife

        public SupernovaGameEventConfig()
        {
            GameEventType = GameEventType.Supernova;
        }

        #endregion

    }
}