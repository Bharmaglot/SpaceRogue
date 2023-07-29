using SpaceRogue.Gameplay.GameEvent.Scriptables;
using SpaceRogue.Services;
using System;
using UnityEngine;
using Utilities.Mathematics;


namespace SpaceRogue.Gameplay.GameEvent.Comet
{
    public sealed class Comet : IDisposable
    {

        #region Fields

        private readonly Updater _updater;
        private readonly CometConfig _cometConfig;
        private readonly Vector3 _movementDirection;
        private readonly float _speed;
        private float _remainingLifeTime;

        #endregion


        #region Properties

        public bool IsDestroyed { get; private set; } = false;

        public CometView CometView { get; private set; }

        #endregion


        #region CodeLife

        public Comet(Updater updater, CometConfig cometConfig, CometView cometView, Vector3 movementDirection)
        {
            _updater = updater;
            _cometConfig = cometConfig;
            CometView = cometView;
            _movementDirection = movementDirection;

            _speed = RandomPicker.PickRandomBetweenTwoValues(_cometConfig.MinSpeed, _cometConfig.MaxSpeed);
            _remainingLifeTime = cometConfig.LifeTime;

            CometView.CollidedSpaceObject += Dispose;
            CometView.CollidedPlanet += Dispose;

            _updater.SubscribeToUpdate(Move);
        }

        public void Dispose()
        {
            CometView.CollidedPlanet -= Dispose;
            CometView.CollidedSpaceObject -= Dispose;
            _updater.UnsubscribeFromUpdate(Move);
            IsDestroyed = true;

            if (CometView != null)
            {
                UnityEngine.Object.Destroy(CometView.gameObject); 
            }
        }

        #endregion


        #region Methods

        private void Move(float deltaTime)
        {
            if (_remainingLifeTime <= 0)
            {
                Dispose();
                return;
            }

            CometView.Rigidbody2D.AddForce(_movementDirection.normalized * _speed, ForceMode2D.Force);

            _remainingLifeTime -= deltaTime;
        }

        #endregion

    }
}