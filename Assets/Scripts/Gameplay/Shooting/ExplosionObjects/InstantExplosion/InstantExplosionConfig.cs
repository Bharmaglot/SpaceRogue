using SpaceRogue.Enums;
using SpaceRogue.Gameplay.Shooting;
using SpaceRogue.Gameplay.Shooting.Scriptables;
using UnityEngine;


[CreateAssetMenu(fileName = nameof(InstantExplosionConfig), menuName = "Configs/Projectiles/" + nameof(InstantExplosionConfig))]
public class InstantExplosionConfig : BaseExplosionObjectConfig
{
    [field: SerializeField] public ExplosionView Prefab { get; private set; }
    [field: SerializeField, Min(0.1f)] public float LifeTime { get; private set; }

    public InstantExplosionConfig()
    {
        Type = ExplosionEffectType.InstantExplosion;
    }
}
