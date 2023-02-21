using Gameplay.Damage;
using UnityEngine;

namespace Gameplay.Space.Star
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class DamageZoneView : MonoBehaviour, IRepeatableDamageView, IDamagingView
    {
        public float CooldownDamage { get; set; }
        public DamageModel DamageModel { get; private set; }

        public void Init(DamageModel damageModel)
        {
            DamageModel = damageModel;
        }
    }
}