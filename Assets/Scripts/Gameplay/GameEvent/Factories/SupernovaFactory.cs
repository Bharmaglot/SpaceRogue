using Gameplay.Mechanics.Timer;
using Gameplay.Space.SpaceObjects;
using SpaceRogue.Scriptables.GameEvent;
using SpaceRogue.Services;
using Zenject;


namespace SpaceRogue.Gameplay.GameEvent.Factories
{
    public sealed class SupernovaFactory : PlaceholderFactory<SupernovaGameEventConfig, SpaceObjectView, Supernova.Supernova>
    {

        #region Fields

        private readonly Updater _updater;
        private readonly TimerFactory _timerFactory;

        #endregion


        #region CodeLife

        public SupernovaFactory(Updater updater, TimerFactory timerFactory)
        {
            _updater = updater;
            _timerFactory = timerFactory;
        }

        #endregion


        #region Methods

        public override Supernova.Supernova Create(SupernovaGameEventConfig config, SpaceObjectView spaceObjectView)
            => new(_updater, config, spaceObjectView, _timerFactory);

        #endregion

    }
}