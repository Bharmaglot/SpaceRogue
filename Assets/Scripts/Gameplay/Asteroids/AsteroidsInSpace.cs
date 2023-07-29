using System;
using System.Collections.Generic;
using Gameplay.Asteroids.Factories;
using Gameplay.Asteroids.Scriptables;
using Gameplay.Space.Generator;
using UnityEngine;
using Utilities.Mathematics;

namespace Gameplay.Asteroids
{
    public class AsteroidsInSpace : IDisposable
    {
        private readonly SpawnPointsFinder _spawnPointsFinder;
        private readonly AsteroidFactory _asteroidFactory;
        private readonly int _asteroidsOnStartCount;
        private readonly AsteroidSpawnConfig _spawnConfig;
        private readonly List<Asteroid> _asteroids = new();

        private const int MaxSpawnTriesPerAsteroid = 5;
        private const int MaxTriesToCreateStartAsteroids = 100;


        public AsteroidsInSpace(
            int asteroidsOnStartCount, 
            AsteroidSpawnConfig spawnConfig, 
            SpawnPointsFinder spawnPointsFinder, 
            AsteroidFactory asteroidFactory)
        {
            _asteroidsOnStartCount = asteroidsOnStartCount;
            _spawnConfig = spawnConfig;
            _spawnPointsFinder = spawnPointsFinder;
            _asteroidFactory = asteroidFactory;
        }

        public void Dispose()
        {
            foreach (var asteroid in _asteroids)
            {
                asteroid?.Dispose();
            }
            _asteroids.Clear();
        }

        public void SpawnStartAsteroids()
        {
            var tryCount = 0;
            do
            {
                TrySpawnAsteroid(_spawnConfig);
                tryCount++;
            }
            while (_asteroids.Count < _asteroidsOnStartCount || tryCount < MaxTriesToCreateStartAsteroids);
        }

        private void TrySpawnAsteroid(AsteroidSpawnConfig config)
        {
            var newAsteroidConfig = RandomPicker.PickOneElementByWeights(config.AsteroidConfigs);
            var asteroidCollider = newAsteroidConfig.Prefab.GetComponent<CircleCollider2D>();
            var asteroidRadius = asteroidCollider.radius;
            var asteroidSpawnRadius = RandomPicker.PickRandomBetweenTwoValues(0, newAsteroidConfig.MaxSpawnRadius);
            var spawnTries = 0;
            do
            {
                if (_spawnPointsFinder.TryGetSpaceObjectSpawnPoint(asteroidRadius, asteroidSpawnRadius, out var spawnPoint))
                {
                    var newAsteroid = _asteroidFactory.Create(spawnPoint, newAsteroidConfig);
                    _asteroids.Add(newAsteroid);
                    return;
                }
                spawnTries++;
            } 
            while (spawnTries < MaxSpawnTriesPerAsteroid);
        }
    }
}