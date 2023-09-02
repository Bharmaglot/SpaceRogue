using SpaceRogue.Enums;
using UnityEngine;


namespace SpaceRogue.Gameplay.Shooting.Scriptables
{
    [CreateAssetMenu(fileName = nameof(WaveExplosionConfig), menuName = "Configs/Projectiles/" + nameof(WaveExplosionConfig))]
    public sealed class WaveExplosionConfig : BaseWaveExplosionConfig
    {
        [field: SerializeField] public ExplosionView Prefab { get; private set; }

        public WaveExplosionConfig()
        {
            Type = ExplosionEffectType.WaveExplosion;
        }
    }
}