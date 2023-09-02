//using SpaceRogue.Abstraction;
//using SpaceRogue.Services;
//using System;
//using UnityEngine;

//namespace SpaceRogue.Gameplay.Shooting
//{
//    public abstract class BaseExplosionObject : IDisposable
//    {
//        //To Do childrens
//        #region Fields

//        private readonly Updater _updater;
//        private readonly IDestroyable _destroyable;

//        private readonly Transform _explosionTransform;

//        private readonly float _dangerRadius;
//        private readonly float _speedWaveExplosion;

//        #endregion


//        #region CodeLife

//        public BaseExplosionObject(Updater updater, IDestroyable destroyable)
//        {
//            _updater = updater;
//            _destroyable = destroyable;
//        }

//        public virtual void Dispose()
//        {
//            _destroyable.Destroyed -= Dispose;
//        }

//        #endregion


//        #region Methods

//        private void ExplosionEffect()
//        {
//            if (_explosionTransform.localScale.x < _dangerRadius)
//            {
//                _explosionTransform.localScale = 
//                new Vector2(_explosionTransform.localScale.x + _speedWaveExplosion * Time.deltaTime,
//                _explosionTransform.localScale.y + _speedWaveExplosion * Time.deltaTime);
//            }
//            else
//            {
//                Dispose();
//            }
//        }

//        #endregion

//    }
//}
