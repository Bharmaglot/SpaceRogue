using Gameplay.Damage;
using Gameplay.Space.Planet;
using Gameplay.Space.Star;
using System;
using UnityEngine;

namespace Gameplay.GameEvent
{
    [RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D), typeof(TrailRenderer))]
    public sealed class CometView : MonoBehaviour, IDamagingView
    {
        public event Action CollisionEnter = () => { };
        public DamageModel DamageModel { get; private set; }

        public void Init(DamageModel damageModel)
        {
            DamageModel = damageModel;
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out SpaceObjectView starView) || collision.gameObject.TryGetComponent(out PlanetView planetView))
            {
                CollisionEnter();
            }
        }
    }
}