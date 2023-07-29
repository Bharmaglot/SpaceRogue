using Gameplay.Damage;
using Gameplay.Player;
using Gameplay.Space.Planets;
using Gameplay.Space.SpaceObjects;
using Gameplay.Survival;
using System;
using UnityEngine;


namespace SpaceRogue.Gameplay.GameEvent.Comet
{
    [RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D), typeof(TrailRenderer))]
    public sealed class CometView : MonoBehaviour, IDamagingView
    {

        #region Events

        public event Action CollidedSpaceObject = () => { };

        public event Action CollidedPlanet = () => { };

        #endregion


        #region Properties

        [field: SerializeField] public Rigidbody2D Rigidbody2D { get; private set; }
        [field: SerializeField] public Collider2D Collider2D { get; private set; }
        [field: SerializeField] public TrailRenderer TrailRenderer { get; private set; }
        public DamageModel DamageModel { get; private set; }

        #endregion


        #region Mono

        private void OnValidate()
        {
            Rigidbody2D ??= GetComponent<Rigidbody2D>();
            Collider2D ??= GetComponent<Collider2D>();
            TrailRenderer ??= GetComponent<TrailRenderer>();
        }

        #endregion


        #region CodeLife
        
        public void Init(DamageModel damageModel) => DamageModel = damageModel; 
        
        #endregion


        #region Methods

        public void DealDamage(IDamageableView damageable) => damageable.TakeDamage(DamageModel);

        public void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out PlanetView _))
            {
                CollidedPlanet();
                return;
            }
            else if (collision.gameObject.TryGetComponent(out SpaceObjectView _))
            {
                CollidedSpaceObject();
                return;
            }
            else if (collision.gameObject.TryGetComponent(out PlayerView playerView))
            {
                DealDamage(playerView);
            }
        }

        #endregion
    }
}