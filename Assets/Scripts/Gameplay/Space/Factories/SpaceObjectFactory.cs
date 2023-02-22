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
        private readonly PlanetSystemOfTypeConfig _planetSystemOfTypeConfig;
        private readonly GravityOfTypeConfig _gravityOfTypeConfig;
        private readonly RepeatableDamageOfTypeConfig _repeatebleDamageOfTypeConfig;

        private PlanetarySystemFactory _planetarySystemFactory;
        private GravityAreaFactory _gravityAreaFactory;
        private DamageZoneAreaFactory _damageZoneAreaFactory;


        public SpaceObjectFactory(StarSpawnConfig starSpawnStarSpawnConfig, PlanetSpawnConfig planetSpawnConfig,
            GravityOfTypeConfig gravityOfTypeConfig, PlanetSystemOfTypeConfig planetSystemOfTypeConfig, RepeatableDamageOfTypeConfig repeatableDamageOfTypeConfig)
        {
            _starSpawnConfig = starSpawnStarSpawnConfig;
            _planetSpawnConfig = planetSpawnConfig;
            _random = new System.Random();
            _gravityOfTypeConfig = gravityOfTypeConfig;
            _planetSystemOfTypeConfig = planetSystemOfTypeConfig;
            _repeatebleDamageOfTypeConfig = repeatableDamageOfTypeConfig;
        }

        public (StarController, PlanetController[], GravityController, DamageZoneController) CreateStarSystem(Vector3 starSpawnPosition, Transform starsParent)
        {
            var config = PickStar(_starSpawnConfig.WeightConfigs, _random);
            float starSize = RandomPicker.PickRandomBetweenTwoValues(config.MinSize, config.MaxSize, _random);
            var starView = CreateStarView(config.Prefab, starSize, starSpawnPosition);
            var TypeObject = config.Type;
            PlanetController[] planets = null;
            GravityController gravity = null;
            DamageZoneController damageZone = null;

            _gravityAreaFactory = new GravityAreaFactory(starSpawnPosition, starSize);
            _damageZoneAreaFactory = new DamageZoneAreaFactory(starSpawnPosition, starSize);
            _planetarySystemFactory = new PlanetarySystemFactory(starView, starSize, _planetSpawnConfig, starSpawnPosition);

            switch (config.Type)
            {
                case SpaceObjectType.BlackHole:
                    gravity = _gravityAreaFactory.CreateGravityArea(starsParent, _gravityOfTypeConfig.BlackHoleConfig);
                    damageZone = _damageZoneAreaFactory.CreateDamageZone(starsParent, _repeatebleDamageOfTypeConfig.BlackHoleConfig);
                    break;
                case SpaceObjectType.SunStar:
                    planets = _planetarySystemFactory.CreatePlanetarySystem(_planetSystemOfTypeConfig.SunStarConfig);
                    break;
                case SpaceObjectType.WhiteDwarf:
                    planets = _planetarySystemFactory.CreatePlanetarySystem(_planetSystemOfTypeConfig.WhiteDwarfConfig);
                    break;
                case SpaceObjectType.RedGiant:
                    planets = _planetarySystemFactory.CreatePlanetarySystem(_planetSystemOfTypeConfig.RedGiantConfig);
                    break;
                case SpaceObjectType.Magnetar:
                    gravity = _gravityAreaFactory.CreateGravityArea(starsParent, _gravityOfTypeConfig.MagnetarConfig);
                    damageZone = _damageZoneAreaFactory.CreateDamageZone(starsParent, _repeatebleDamageOfTypeConfig.MagnetarConfig);
                    break;
                default:
                    Debug.Log("Type not recognized");
                    break;
            }
            return (new StarController(starView, starsParent), planets, gravity, damageZone);

        }

        private static StarView CreateStarView(StarView prefab, float size, Vector3 spawnPosition)
        {
            var viewGo = Object.Instantiate(prefab, spawnPosition, Quaternion.identity);
            viewGo.transform.localScale = new Vector3(size, size);
            return viewGo;
        }
        private static StarConfig PickStar(List<WeightConfig<StarConfig>> weightConfigs, System.Random random) 
            => RandomPicker.PickOneElementByWeights(weightConfigs, random);
    }
}
