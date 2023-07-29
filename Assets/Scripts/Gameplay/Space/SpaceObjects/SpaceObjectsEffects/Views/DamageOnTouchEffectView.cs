using Gameplay.Damage;
using Gameplay.Survival;
using UnityEngine;


namespace Gameplay.Space.SpaceObjects.SpaceObjectsEffects.Views
{
    [RequireComponent(typeof(SpriteRenderer))]
    public sealed class DamageOnTouchEffectView : AreaEffectView, IDamagingView
    {

        #region Properties

        [field: SerializeField] public SpriteRenderer SpriteRenderer { get; private set; }

        public DamageModel DamageModel { get; private set; }

        #endregion


        #region Mono

        private void OnValidate() => SpriteRenderer ??= GetComponent<SpriteRenderer>();

        private void OnTriggerEnter2D(Collider2D other) => CollisionEnter(other.gameObject);

        private void OnCollisionEnter2D(Collision2D collision) => CollisionEnter(collision.gameObject);

        #endregion


        #region CodeLife

        public void Init(DamageModel damageModel) => DamageModel = damageModel; 
        
        #endregion


        #region Methods

        public void DealDamage(IDamageableView damageable) => damageable.TakeDamage(DamageModel);

        private void CollisionEnter(GameObject go)
        {
            var damageable = go.GetComponent<IDamageableView>();
            if (damageable is not null)
            {
                DealDamage(damageable);
            }
        }

        #endregion

    }
}