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
        private float _cooldown;

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
            _cooldown = repeatableDamageComponent.CooldownDamage;
            _dZTimer = new(_cooldown);
            _dZTimer.Start();
            _cooldownDamageComponent = damageComponent;
            EntryPoint.SubscribeToUpdate(TimeToDamage);

        }
        public void StopTakeRepetableDamage()
        {
            _dZTimer.Dispose();
        }

        public void TimeToDamage()
        {
            if (_dZTimer.CurrentValue < 1f)
            {
                _dZTimer.UpdateValue(_cooldown);
                TakeDamage(_cooldownDamageComponent);

            }
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
                EntryPoint.UnsubscribeFromUpdate(TimeToDamage);
            }
        }


    }
}