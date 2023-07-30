using Gameplay.Mechanics.Timer;
using Gameplay.Space.SpaceObjects;
using SpaceRogue.Gameplay.GameEvent.Factories;
using SpaceRogue.Gameplay.Player;
using SpaceRogue.Scriptables.GameEvent;
using System.Collections.Generic;
using UnityEngine;


namespace SpaceRogue.Gameplay.GameEvent.Supernova
{
    public sealed class SupernovaGameEvent : GameEvent
    {

        #region Fields

        private readonly SupernovaGameEventConfig _supernovaGameEventConfig;
        private readonly PlayerView _playerView;
        private readonly SupernovaFactory _supernovaFactory;

        private readonly List<Supernova> _supernovas = new();

        #endregion


        #region CodeLife

        public SupernovaGameEvent(
            SupernovaGameEventConfig config,
            TimerFactory timerFactory,
            PlayerView playerView,
            SupernovaFactory supernovaFactory) : base(config, timerFactory)
        {
            _supernovaGameEventConfig = config;
            _playerView = playerView;
            _supernovaFactory = supernovaFactory;
        }

        protected override void OnDispose()
        {
            foreach (var supernova in _supernovas)
            {
                supernova.Dispose();
            }
            _supernovas.Clear();
        }

        #endregion


        #region Methods

        protected override bool RunGameEvent()
        {
            for (var i = 0; i < _supernovas.Count; i++)
            {
                if (_supernovas[i].IsDestroyed)
                {
                    _supernovas.Remove(_supernovas[i]);
                }
            }

            if (!TryGetNearestSpaceObjectView(
                _playerView.transform.position,
                _supernovaGameEventConfig.SearchRadius,
                out var spaceObjectView))
            {
                return false;
            }

            var supernova = _supernovaFactory.Create(_supernovaGameEventConfig, spaceObjectView);
            AddGameEventIndicator(spaceObjectView.CircleCollider2D);
            _supernovas.Add(supernova);

            return _supernovas.Count != 0;
        }

        private bool TryGetNearestSpaceObjectView(Vector3 position, float radius, out SpaceObjectView spaceObjectView)
        {
            spaceObjectView = null;
            
            var colliders = Physics2D.OverlapCircleAll(position, radius);
            var views = new List<SpaceObjectView>();
            foreach (var item in colliders)
            {
                if (item.TryGetComponent(out SpaceObjectView view))
                {
                    if (!view.InGameEvent && view.IsVisibleForGameEvents)
                    {
                        views.Add(view);
                    }
                }
            }

            if (views.Count == 0)
            {
                return false;
            }

            spaceObjectView = GetClosestStarView(views, position);

            return true;

        }

        private SpaceObjectView GetClosestStarView(List<SpaceObjectView> spaceObjectViews, Vector3 currentPosition)
        {
            var view = default(SpaceObjectView);
            var closestDistanceSqr = Mathf.Infinity;

            for (var i = 0; i < spaceObjectViews.Count; i++)
            {
                var direction = spaceObjectViews[i].transform.position - currentPosition;
                var sqrMagnitude = direction.sqrMagnitude;

                if (sqrMagnitude < closestDistanceSqr)
                {
                    closestDistanceSqr = sqrMagnitude;
                    view = spaceObjectViews[i];
                }
            }
            view.MarkInGameEvent();

            return view;
        }

        #endregion

    }
}