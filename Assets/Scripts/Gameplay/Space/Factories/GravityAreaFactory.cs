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
        private GravityConfig _gravityConfig;

        public GravityAreaFactory(Vector3 starSpawnPosition, float objectSize)
        {
            _spawnPosition = starSpawnPosition;
            _centerObjectSize = objectSize;
            
        }

        public GravityController CreateGravityArea(Transform starsParent, GravityConfig gravityConfig)
        {
            var gravityPosition = _spawnPosition;
            float gravityAreaSize = _centerObjectSize * gravityConfig.RadiusGravity;
            var gravityView = CreateGravityView(gravityConfig.Prefab, gravityAreaSize, gravityPosition);
            return (new GravityController(gravityView, starsParent, gravityConfig));
        }


        public static GravityView CreateGravityView(GravityView prefab, float size, Vector3 spawnPosition)
        {
            var viewGo = Object.Instantiate(prefab, spawnPosition, Quaternion.identity);
            viewGo.transform.localScale = new Vector3(size, size);
            return viewGo;
        }
    }
}