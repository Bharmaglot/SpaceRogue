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
    public sealed class PlanetarySystemFactory 
{
        private readonly SpaceObjectView _spaceObjectView;
        private readonly System.Random _random;
        private readonly float _starSize;
        private readonly PlanetSpawnConfig _planetSpawnConfig;
        private readonly Vector3 _starSpawnPosition;

        public PlanetarySystemFactory(SpaceObjectView spaceObjectView, float spaceObjectSize, PlanetSpawnConfig planetSpawnConfig, Vector3 starSpawnPosition)
        {
            _spaceObjectView = spaceObjectView;
            
            _random = new System.Random();
            _starSize = spaceObjectSize;
            _planetSpawnConfig = planetSpawnConfig;
            _starSpawnPosition = starSpawnPosition;
        }

        public PlanetController[] CreatePlanetarySystem(PlanetarySystemConfig planetarySystemConfig)
        {
            int planetCount = RandomPicker.PickRandomBetweenTwoValues(planetarySystemConfig.MinPlanetCount, planetarySystemConfig.MaxPlanetCount, _random);
            var planets = new PlanetController[planetCount];

            if (planetCount <= 0) return (new PlanetController[planetCount]);

            float[] planetOrbits = GetPlanetOrbitList(planetCount, planetarySystemConfig.MinOrbit, planetarySystemConfig.MaxOrbit, _starSize, _random);

            for (int i = 0; i < planets.Length; i++)
            {
                var planetConfig = PickPlanet(_planetSpawnConfig.WeightConfigs, _random);
                float planetSize = RandomPicker.PickRandomBetweenTwoValues(planetConfig.MinSize, planetConfig.MaxSize, _random);
                float planetSpeed = RandomPicker.PickRandomBetweenTwoValues(planetConfig.MinSpeed, planetConfig.MaxSpeed, _random);
                float planetDamage = RandomPicker.PickRandomBetweenTwoValues(planetConfig.MinDamage, planetConfig.MaxDamage, _random);
                bool isPlanetMovingRetrograde = RandomPicker.TakeChance(planetConfig.RetrogradeMovementChance, _random);
                var planetView = CreatePlanetView(planetConfig.Prefab, planetSize, _starSize, planetOrbits[i], _starSpawnPosition);
                planets[i] = new PlanetController(planetView, _spaceObjectView, planetSpeed, isPlanetMovingRetrograde, planetDamage);
            }
            return planets;
        }

        private static float[] GetPlanetOrbitList(int planetCount, float minOrbit, float maxOrbit, float starSize, System.Random random)
        {
            var orbits = new float[planetCount];
            float realMinOrbit = starSize / 2 + minOrbit;
            float realMaxOrbit = starSize / 2 + maxOrbit;

            if (planetCount == 1)
            {
                orbits[0] = RandomPicker.PickRandomBetweenTwoValues(realMinOrbit, realMaxOrbit, random);
                return orbits;
            }

            float orbitChunk = realMaxOrbit - realMinOrbit / planetCount;

            for (int i = 0; i < planetCount; i++)
            {
                float minChunkOrbit = realMinOrbit + i * orbitChunk;
                float maxChunkOrbit = realMinOrbit + (i + 1) * orbitChunk;
                orbits[i] = RandomPicker.PickRandomBetweenTwoValues(minChunkOrbit, maxChunkOrbit, random);
            }

            return orbits;
        }

        private static PlanetConfig PickPlanet(List<WeightConfig<PlanetConfig>> weightConfigs, System.Random random) =>
    RandomPicker.PickOneElementByWeights(weightConfigs, random);


        private static PlanetView CreatePlanetView(PlanetView prefab, float size, float starSize, float orbit, Vector3 starPosition)
        {
            var planetSpawnPosition = starPosition + new Vector3(0, starSize + orbit + size / 2, 0);
            var viewGo = Object.Instantiate(prefab, planetSpawnPosition, Quaternion.identity);
            viewGo.transform.localScale = new Vector3(size, size);
            return viewGo;
        }
    }
}
