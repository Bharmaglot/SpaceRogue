using Gameplay.Mechanics.Timer;
using SpaceRogue.Gameplay.Shooting.Scriptables;
using System;
using Object = UnityEngine.Object;


namespace SpaceRogue.Gameplay.Shooting
{
    public sealed class Projectile : IDisposable
    {

        #region Fields

        private readonly ProjectileView _projectileView;
        private readonly Timer _lifeTime;

        private bool _disposing;

        #endregion


        #region CodeLife

        public Projectile(ProjectileConfig config, ProjectileView projectileView, TimerFactory timerFactory)
        {
            _projectileView = projectileView;

            _lifeTime = timerFactory.Create(config.LifeTime);
            _lifeTime.OnExpire += Dispose;

            if (config.IsDestroyedOnHit)
            {
                _projectileView.CollidedObject += Dispose;
            }

            _lifeTime.Start();
        }

        public void Dispose()
        {
            if (_disposing)
            {
                return;
            }

            _disposing = true;

            _lifeTime.OnExpire -= Dispose;
            _projectileView.CollidedObject -= Dispose;
            _lifeTime.Dispose();

            Object.Destroy(_projectileView.gameObject);
        }

        #endregion

    }
}