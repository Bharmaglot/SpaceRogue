using Gameplay.Mechanics.Timer;
using Zenject;


namespace SpaceRogue.Gameplay.Shooting.Factories
{
    public sealed class ProjectileFactory : PlaceholderFactory<ProjectileSpawnParams, Projectile>
    {

        #region Fields

        private readonly ProjectileViewFactory _projectileViewFactory;
        private readonly TimerFactory _timerFactory;

        #endregion


        #region CodeLife

        public ProjectileFactory(ProjectileViewFactory projectileViewFactory, TimerFactory timerFactory)
        {
            _projectileViewFactory = projectileViewFactory;
            _timerFactory = timerFactory;
        }

        #endregion


        #region Methods

        public override Projectile Create(ProjectileSpawnParams spawnParams)
        {
            var projectileView = _projectileViewFactory.Create(spawnParams);
            var projectile = new Projectile(spawnParams.Config, spawnParams.Weapon, projectileView, _timerFactory);
            return projectile;
        }

        #endregion

    }
}