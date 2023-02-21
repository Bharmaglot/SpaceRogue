using System.Collections.Generic;
using Gameplay.Space.Planet;
using Gameplay.Space.Star;
using Scriptables;
using Scriptables.Space;
using UnityEngine;
using Utilities.Mathematics;
using Object = UnityEngine.Object;
using Abstracts;

namespace Gameplay.Space
{
    public sealed class DamageZoneAreaFactory
    {
        private readonly Vector3 _spawnPosition;
        private readonly float _centerObjectSize;
        private readonly RepeatableDamageConfig _damageConfig;

        public DamageZoneAreaFactory(Vector3 starSpawnPosition, float starSize, RepeatableDamageConfig damageConfig)
        {
            _spawnPosition = starSpawnPosition;
            _centerObjectSize = starSize;
            _damageConfig = damageConfig;
        }

        public DamageZoneController CreateDamageZone(Transform starsParent)
        {
            var damageZonePosition = _spawnPosition;
            float damageZoneSize = _centerObjectSize * _damageConfig.DamageSize;
            var damageZoneView = CreateDamageZoneView(_damageConfig.Prefab, damageZoneSize, _spawnPosition);
            return (new DamageZoneController(damageZoneView, starsParent, _damageConfig.DamageValue, _damageConfig.DamageCooldown));
        }

        public DamageZoneView CreateDamageZoneView(DamageZoneView prefab, float size, Vector3 spawnPosition)
        {
            var viewGo = Object.Instantiate(prefab, spawnPosition, Quaternion.identity);
            viewGo.transform.localScale = new Vector3(size, size);
            return viewGo;
        }
    }
}
