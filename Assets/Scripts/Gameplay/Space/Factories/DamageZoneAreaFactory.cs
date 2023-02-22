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
        private RepeatableDamageConfig _damageConfig;

        public DamageZoneAreaFactory(Vector3 spawnPosition, float starSize)
        {
            _spawnPosition = spawnPosition;
            _centerObjectSize = starSize;
        }

        public DamageZoneController CreateDamageZone(Transform spaceObjectParent, RepeatableDamageConfig damageConfig)
        {
            var damageZonePosition = _spawnPosition;
            float damageZoneSize = _centerObjectSize * damageConfig.DamageSize;
            var damageZoneView = CreateDamageZoneView(damageConfig.Prefab, damageZoneSize, _spawnPosition);
            return (new DamageZoneController(damageZoneView, spaceObjectParent, damageConfig.DamageValue, damageConfig.DamageCooldown));
        }

        public DamageZoneView CreateDamageZoneView(DamageZoneView prefab, float size, Vector3 spawnPosition)
        {
            var viewGo = Object.Instantiate(prefab, spawnPosition, Quaternion.identity);
            viewGo.transform.localScale = new Vector3(size, size);
            return viewGo;
        }
    }
}
