using Gameplay.Mechanics.Timer;
using Gameplay.Shooting.Scriptables;
using SpaceRogue.Services;
using UnityEngine;
using Zenject;

namespace Gameplay.Shooting.Factories
{
    public class MineFactory : PlaceholderFactory<Vector2, MineConfig, Mine>
    {

        private readonly MineViewFactory _mineViewFactory;
        private readonly TimerFactory _timerFactory;

        private readonly Updater _updater;

        public MineFactory(MineViewFactory mineViewFactory, TimerFactory timerFactory, Updater updater)
        {
            _mineViewFactory = mineViewFactory;
            _timerFactory = timerFactory;
            _updater = updater;
        }

        public override Mine Create(Vector2 position, MineConfig config)
        {
            var mineView = _mineViewFactory.Create(position, config);
            var mine = new Mine(_updater, mineView, _timerFactory, config);
            return mine;
        }
    }
}
