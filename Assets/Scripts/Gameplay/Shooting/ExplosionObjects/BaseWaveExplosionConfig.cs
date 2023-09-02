using SpaceRogue.Gameplay.Shooting.Scriptables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWaveExplosionConfig : BaseExplosionObjectConfig
{
    [field: SerializeField, Min(0.1f)] public float SpeedWaveExplosion { get; private set; }
}
