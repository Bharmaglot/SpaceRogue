using System;
using Gameplay.Mechanics.Timer;
using Gameplay.Shooting.Scriptables;
using Object = UnityEngine.Object;

namespace Gameplay.Shooting
{
    public sealed class Projectile : IDisposable
    {
        private readonly ProjectileView _projectileView;
        private readonly Timer _lifeTime;
        
        public Projectile(ProjectileConfig config, ProjectileView projectileView, TimerFactory timerFactory)
        {
            _projectileView = projectileView;

            _lifeTime = timerFactory.Create(config.LifeTime);
            _lifeTime.OnExpire += Dispose;
            if (config.IsDestroyedOnHit) _projectileView.CollidedObject += Dispose;
            
            _lifeTime.Start();
        }

        public void Dispose()
        {
            _lifeTime.OnExpire -= Dispose;
            _projectileView.CollidedObject -= Dispose;
            _lifeTime.Dispose();
            
            if (_projectileView is not null)
            {
                Object.Destroy(_projectileView.gameObject);
            }
            
        }
    }
}