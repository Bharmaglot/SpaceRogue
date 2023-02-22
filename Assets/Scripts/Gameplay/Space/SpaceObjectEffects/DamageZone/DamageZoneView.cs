using Gameplay.Damage;
using UnityEngine;

namespace Gameplay.Space.Star
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class DamageZoneView : MonoBehaviour, IRepeatableDamageView
    {
        public float DamageCooldown { get; set; }
        public DamageModel DamageModel { get; private set; }

        public void Init(DamageModel damageModel)
        {
            DamageModel = damageModel;
        }
    }
}