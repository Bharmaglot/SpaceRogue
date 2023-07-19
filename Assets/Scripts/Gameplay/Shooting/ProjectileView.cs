using Gameplay.Damage;
using Gameplay.Survival;
using System;
using UnityEngine;


namespace SpaceRogue.Gameplay.Shooting
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider))]
    public sealed class ProjectileView : MonoBehaviour, IDamagingView
    {
        #region Events

        public event Action CollidedObject = () => { };

        #endregion

        #region Properties

        public DamageModel DamageModel { get; private set; }

        #endregion

        #region Mono

        private void OnTriggerEnter2D(Collider2D other) => CollisionEnter(other.gameObject);

        private void OnCollisionEnter2D(Collision2D collision) => CollisionEnter(collision.gameObject);

        #endregion

        #region CodeLife

        public void Init(DamageModel damageModel) => DamageModel = damageModel; 
        
        #endregion

        #region Methods

        public void DealDamage(IDamageableView damageable) => damageable.TakeDamage(DamageModel);

        private void CollisionEnter(GameObject gameObject)
        {
            if(gameObject.TryGetComponent(out IDamageableView damageable))
            {
                DealDamage(damageable);
            }

            CollidedObject.Invoke();
        }

        #endregion
    }
}