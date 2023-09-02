using Gameplay.Damage;
using Gameplay.Survival;
using UnityEngine;


namespace SpaceRogue.Gameplay.Shooting
{
    public sealed class DamageExplosionView : MonoBehaviour, IDamagingView
    {

        #region Fields

        public DamageModel DamageModel { get; private set; }

        #endregion


        #region Methods

        public void Init(DamageModel damageModel)
        {
            DamageModel = damageModel;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IDamageableView damageable))
            {
                DealDamage(damageable);
            }
        }

        public void DealDamage(IDamageableView damageable)
        {
            damageable.TakeDamage(DamageModel);
        }

        #endregion

    }
}