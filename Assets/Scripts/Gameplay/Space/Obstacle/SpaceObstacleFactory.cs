using System;
using Zenject;


namespace SpaceRogue.Gameplay.Space.Obstacle
{
    public sealed class SpaceObstacleFactory : PlaceholderFactory<SpaceObstacleView, float, SpaceObstacle>
    {
        public event Action<SpaceObstacle> SpaceObstacleCreated;

        public override SpaceObstacle Create(SpaceObstacleView spaceObstacleView, float obstacleForce)
        {
            var spaceObstacle = base.Create(spaceObstacleView, obstacleForce);
            SpaceObstacleCreated?.Invoke(spaceObstacle);
            return spaceObstacle;
        }
    }
}