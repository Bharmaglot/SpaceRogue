using Gameplay.GameProgress;
using Gameplay.Mechanics.Timer;
using SpaceRogue.Gameplay.Shooting.Scriptables;
using SpaceRogue.Services;
using UnityEngine;
using Zenject;


namespace SpaceRogue.Gameplay.Shooting.Factories
{
    public sealed class MineFactory : PlaceholderFactory<Vector2, MineConfig, Weapon, Mine>
    {

        #region Fields

        private readonly MineViewFactory _mineViewFactory;
        private readonly TimerFactory _timerFactory;
        private readonly Updater _updater;

        #endregion


        #region CodeLife

        public MineFactory(MineViewFactory mineViewFactory, TimerFactory timerFactory, Updater updater)
        {
            _mineViewFactory = mineViewFactory;
            _timerFactory = timerFactory;
            _updater = updater;
        }

        #endregion


        #region Methods

        public override Mine Create(Vector2 position, MineConfig config, Weapon weapon)
        {
            var mineView = _mineViewFactory.Create(position, config);
            var mine = new Mine(_updater, mineView, _timerFactory, config, weapon);
            return mine;
        }

        #endregion

    }
}
