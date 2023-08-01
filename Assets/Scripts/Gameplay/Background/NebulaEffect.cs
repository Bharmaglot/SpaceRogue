using UnityEngine;


namespace SpaceRogue.Gameplay.Background
{
    public sealed class NebulaEffect : ParallaxEffect
    {
        public NebulaEffect(Transform cameraTransform, Transform transform, float coefficient) : base(cameraTransform, transform, coefficient)
        {
        }
    }
}
