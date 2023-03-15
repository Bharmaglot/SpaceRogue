using System;
using Gameplay.Space.SpaceObjects.Scriptables;
using UnityEngine;

namespace Scriptables
{
    [CreateAssetMenu(fileName = nameof(LevelPreset), menuName = "Configs/Level/" + nameof(LevelPreset))]
    public sealed class LevelPreset : ScriptableObject
    {
        [field: SerializeField] public SpaceConfig SpaceConfig { get; private set; }
        [field: SerializeField] public int EnemiesCountToWin { get; private set; } = 10;
    }
}