using System.Collections.Generic;
using Gameplay.Space.Generator;
using Gameplay.Space.SpaceObjects;
using Gameplay.Space.SpaceObjects.Scriptables;
using Utilities.Mathematics;
using Zenject;

namespace Gameplay.Space.Factories
{
    public sealed class SpaceFactory : PlaceholderFactory<int, SpaceView, SpawnPointsFinder, Space>
    {
        private readonly SpaceObjectFactory _spaceObjectFactory;
        private readonly SpaceObjectSpawnConfig _spaceObjectSpawnConfig;

        public SpaceFactory(SpaceObjectFactory spaceObjectFactory, SpaceObjectSpawnConfig spaceObjectSpawnConfig)
        {
            _spaceObjectFactory = spaceObjectFactory;
            _spaceObjectSpawnConfig = spaceObjectSpawnConfig;
        }
        
        public override Space Create(int spaceObjectCount, SpaceView spaceView, SpawnPointsFinder spawnPointsFinder)
        {
            List<SpaceObject> spaceObjects = new();
            for (int i = 0; i < spaceObjectCount; i++)
            {
                var currentObjectConfig = RandomPicker.PickOneElementByWeights(_spaceObjectSpawnConfig.SpaceObjectWeights);
                if (spawnPointsFinder.TryGetSpaceObjectSpawnPoint(currentObjectConfig.MaxSize, 15, out var spawnPoint)) //TODO remove orbit?
                {
                    var spaceObject = _spaceObjectFactory.Create(spawnPoint, currentObjectConfig);
                    spaceObjects.Add(spaceObject);
                }
            }

            return new Space(spaceObjects, spaceView);
        }
    }
}