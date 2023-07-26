using Gameplay.Damage;
using Gameplay.Survival;
using SpaceRogue.Enums;
using System;
using UnityEngine;


namespace SpaceRogue.Abstraction
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class EntityViewBase : MonoBehaviour, IDamageableView
    {

        #region Events

        public event Action<DamageModel> DamageTaken = _ => { };

        public event Action EntityDestroyed = () => { };

        #endregion


        #region Properties

        public abstract EntityType EntityType { get; }

        [field: SerializeField] public Rigidbody2D Rigidbody2D { get; private set; }

        #endregion


        #region Mono

        private void OnValidate() => Rigidbody2D ??= GetComponent<Rigidbody2D>();

        public void OnDestroy() => EntityDestroyed();

        #endregion


        #region Methods

        public void TakeDamage(DamageModel damageModel) => DamageTaken(damageModel);

        #endregion

    }
}