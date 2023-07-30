using UnityEngine;


namespace SpaceRogue.Gameplay.Background
{
    public sealed class GameBackgroundView : MonoBehaviour
    {
        [field: SerializeField] public SpriteRenderer BackSpriteRenderer { get; private set; }
        [field: SerializeField] public SpriteRenderer MidSpriteRenderer { get; private set; }
        [field: SerializeField] public SpriteRenderer ForeSpriteRenderer { get; private set; }
        [field: SerializeField] public ParticleSystem NebulaBackParticleSystem { get; private set; }
        [field: SerializeField] public ParticleSystem NebulaForeParticleSystem { get; private set; }
        [field: SerializeField] public ParticleSystem NebulaMaskParticleSystem { get; private set; }
    }
}