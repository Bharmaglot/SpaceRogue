using UnityEngine;


namespace SpaceRogue.Gameplay.Shooting
{
    public abstract class BaseExplosionObjectView : MonoBehaviour
    {
        [field: SerializeField] public Transform ExplosionTransform { get; private set; }
        [field: SerializeField] public DamageExplosionView DamageExplosionView { get; private set; }
    }
}
