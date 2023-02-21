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
    public sealed class GravityAreaFactory
    {
        private readonly Vector3 _spawnPosition;
        private readonly float _centerObjectSize;
        private readonly GravityConfig _gravityConfig;

        public GravityAreaFactory(Vector3 starSpawnPosition, float starSize, GravityConfig gravityConfig)
        {
            _spawnPosition = starSpawnPosition;
            _centerObjectSize = starSize;
            _gravityConfig = gravityConfig;
        }

        public GravityController CreateGravityArea(Transform starsParent)
        {

            var gravityPosition = _spawnPosition;
            float gravityAreaSize = _centerObjectSize * _gravityConfig.RadiusGravity;
            var gravityView = CreateGravityView(_gravityConfig.Prefab, gravityAreaSize, gravityPosition);
            return (new GravityController(gravityView, starsParent, _gravityConfig));
        }


        public static GravityView CreateGravityView(GravityView prefab, float size, Vector3 spawnPosition)
        {
            var viewGo = Object.Instantiate(prefab, spawnPosition, Quaternion.identity);
            viewGo.transform.localScale = new Vector3(size, size);
            return viewGo;
        }
    }
}