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
    public sealed class SpaceObjectFactory
    {
        private readonly StarSpawnConfig _starSpawnConfig;
        private readonly PlanetSpawnConfig _planetSpawnConfig;
        private readonly System.Random _random;

        private PlanetarySystemFactory _planetarySystemFactory;
        private GravityAreaFactory _gravityAreaFactory;
        private DamageZoneAreaFactory _damageZoneAreaFactory;


        public SpaceObjectFactory(StarSpawnConfig starSpawnStarSpawnConfig, PlanetSpawnConfig planetSpawnConfig)
        {
            _starSpawnConfig = starSpawnStarSpawnConfig;
            _planetSpawnConfig = planetSpawnConfig;
            _random = new System.Random();
        }

        public (SpaceObjectController, PlanetController[], GravityController, DamageZoneController) CreateStarSystem(Vector3 starSpawnPosition, Transform starsParent)
        {
            var config = PickStar(_starSpawnConfig.WeightConfigs, _random);
            float starSize = RandomPicker.PickRandomBetweenTwoValues(config.MinSize, config.MaxSize, _random);
            var spaceObjectView = CreateSpaceObjectView(config.Prefab, starSize, starSpawnPosition);
            PlanetController[] planets = null;
            GravityController gravity = null;
            DamageZoneController damageZone = null;

            _gravityAreaFactory = new GravityAreaFactory(starSpawnPosition, starSize);
            _damageZoneAreaFactory = new DamageZoneAreaFactory(starSpawnPosition, starSize);
            _planetarySystemFactory = new PlanetarySystemFactory(spaceObjectView, starSize, _planetSpawnConfig, starSpawnPosition);

            foreach (var spaceObjectEffect in config.Effects)
            {
                if (spaceObjectEffect is GravityConfig)
                {
                    GravityConfig gravityEffect = (GravityConfig)spaceObjectEffect;
                    gravity = _gravityAreaFactory.CreateGravityArea(starsParent, gravityEffect);
                }
                else if (spaceObjectEffect is RepeatableDamageConfig)
                {
                    RepeatableDamageConfig repeatableDamageEffect = (RepeatableDamageConfig)spaceObjectEffect;
                    damageZone = _damageZoneAreaFactory.CreateDamageZone(starsParent, repeatableDamageEffect);
                }
                else if (spaceObjectEffect is PlanetarySystemConfig)
                {
                    PlanetarySystemConfig planetarySystemEffect = (PlanetarySystemConfig)spaceObjectEffect;
                    planets = _planetarySystemFactory.CreatePlanetarySystem(planetarySystemEffect);
                }
                else
                {
                    Debug.Log("Uncorrect config");
                }
            }
            return (new SpaceObjectController(spaceObjectView, starsParent), planets, gravity, damageZone);

        }

        private static SpaceObjectView CreateSpaceObjectView(SpaceObjectView prefab, float size, Vector3 spawnPosition)
        {
            var viewGo = Object.Instantiate(prefab, spawnPosition, Quaternion.identity);
            viewGo.transform.localScale = new Vector3(size, size);
            return viewGo;
        }
        private static SpaceObjectConfig PickStar(List<WeightConfig<SpaceObjectConfig>> weightConfigs, System.Random random) 
            => RandomPicker.PickOneElementByWeights(weightConfigs, random);
    }
}
