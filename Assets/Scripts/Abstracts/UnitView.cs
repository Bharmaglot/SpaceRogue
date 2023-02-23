using System;
using Gameplay.Damage;
using Gameplay.Health;
using UnityEngine;
using Gameplay.Mechanics.Timer;

namespace Abstracts
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class UnitView : MonoBehaviour, IDamageableView
    {
        [field: SerializeField] public UnitType UnitType { get; private set; }

        private Timer _dZTimer;
        private IDamagingView _cooldownDamageComponent;

        public event Action<DamageModel> DamageTaken = (DamageModel _) => { };

        public void OnTriggerEnter2D(Collider2D other)
        {
            CollisionEnter(other.gameObject);
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            CollisionEnter(collision.gameObject);
        }

        public void TakeDamage(IDamagingView damageComponent)
        {
            DamageTaken(damageComponent.DamageModel);
        }

        public void TakeRepetableDamage(IRepeatableDamageView repeatableDamageComponent, IDamagingView damageComponent)
        {

            _cooldownDamageComponent = damageComponent;
            _dZTimer = new(repeatableDamageComponent.DamageCooldown);
            _dZTimer.Start();
            _dZTimer.OnExpire += DamageOnTick;

        }
        public void StopTakeRepetableDamage()
        {
            _dZTimer.OnExpire -= DamageOnTick;
            _dZTimer.Dispose();
        }

        public void DamageOnTick()
        {
            TakeDamage(_cooldownDamageComponent);
            _dZTimer.Start();
        }

        private void CollisionEnter(GameObject go)
        {
            var damageComponent = go.GetComponent<IDamagingView>();
            var repeatableDamageComponent = go.GetComponent<IRepeatableDamageView>();
            if (damageComponent is not null)
            {
                TakeDamage(damageComponent);
                if (repeatableDamageComponent is not null)
                {
                    TakeRepetableDamage(repeatableDamageComponent, damageComponent);
                }
            }
        }

        public void OnTriggerExit2D(Collider2D other)
        {
            CollisionExit(other.gameObject);
        }

        public void OnCollisionExit2D(Collision2D collision)
        {
            CollisionExit(collision.gameObject);
        }

        private void CollisionExit(GameObject go)
        {
            var repeatableDamageComponent = go.GetComponent<IRepeatableDamageView>();
            if (repeatableDamageComponent is not null)
            {
                StopTakeRepetableDamage();
            }
        }
    }
}