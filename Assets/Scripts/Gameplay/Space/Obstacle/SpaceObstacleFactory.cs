using System;
using Zenject;


namespace SpaceRogue.Gameplay.Space.Obstacle
{
    public sealed class SpaceObstacleFactory : PlaceholderFactory<SpaceObstacleView, float, SpaceObstacle>
    {
        public event Action<SpaceObstacle> SpaceObstacleCreated;

        public override SpaceObstacle Create(SpaceObstacleView param1, float param2)
        {
            var spaceObstacle = base.Create(param1, param2);
            SpaceObstacleCreated?.Invoke(spaceObstacle);
            return spaceObstacle;
        }
    }
}