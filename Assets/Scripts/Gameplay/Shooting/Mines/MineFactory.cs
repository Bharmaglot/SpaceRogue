using Gameplay.Shooting.Scriptables;
using SpaceRogue.Services;
using UnityEngine;
using Zenject;

namespace Gameplay.Shooting.Factories
{
    public class MineFactory : PlaceholderFactory<Vector2, MineConfig, Mine>
    {

        private readonly MineViewFactory _mineViewFactory;

        private readonly Updater _updater;

        public override Mine Create(Vector2 position, MineConfig config)
        {
            var mineView = _mineViewFactory.Create(position, config);

            return new Mine(_updater, mineView, config);
        }
    }
}
