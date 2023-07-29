using Gameplay.Player;
using SpaceRogue.Scriptables.GameEvent;

namespace Gameplay.GameEvent.Supernova
{
    public sealed class SupernovaGameEventController : LegacyGameEventController
    {
        private readonly SupernovaGameEventConfig _supernovaGameEventConfig;
        private readonly PlayerView _playerView;
        
        private SupernovaController _supernovaController;
        private bool _isStopped;

        public SupernovaGameEventController(GameEventConfig config, PlayerController playerController) : base(config, playerController)
        {
            var supernovaGameEventConfig = config as SupernovaGameEventConfig;
            _supernovaGameEventConfig = supernovaGameEventConfig
                ? supernovaGameEventConfig
                : throw new System.Exception("Wrong config type was provided");

            _playerView = _playerController.View;
        }

        protected override bool RunGameEvent()
        {
            if (_isStopped)
            {
                return true;
            }

            /*if (!TryGetNearestStarView(_playerView.transform.position, _supernovaGameEventConfig.SearchRadius, out var starView))
            {
                return false;
            }*/

            /*_supernovaController = new(_supernovaGameEventConfig, starView);
            _supernovaController.OnDestroy.Subscribe(DestroyController);
            AddController(_supernovaController);
            AddGameEventObjectToUIController(starView.gameObject);*/
            return true;
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            _supernovaController?.OnDestroy.Unsubscribe(DestroyController);
        }

        protected override void OnPlayerDestroyed()
        {
            _isStopped = true;
        }

        private void DestroyController(bool onDestroy)
        {
            if (onDestroy)
            {
                Dispose();
            }
        }

        /*private bool TryGetNearestStarView(Vector3 position, float radius, out StarView starView)
        {
            starView = null;
            var colliders = Physics2D.OverlapCircleAll(position, radius);

            var views = new List<StarView>();
            foreach (var item in colliders)
            {
                if (item.TryGetComponent(out StarView view))
                {
                    if (!view.InGameEvent)
                    {
                        views.Add(view); 
                    }
                }
            }

            if (views.Count == 0)
            {
                return false;
            }

            starView = GetClosestStarView(views, position);
            return true;

        }

        private StarView GetClosestStarView(List<StarView> starViews, Vector3 currentPosition)
        {
            var view = default(StarView);
            var closestDistanceSqr = Mathf.Infinity;

            for (int i = 0; i < starViews.Count; i++)
            {
                var direction = starViews[i].transform.position - currentPosition;
                var sqrMagnitude = direction.sqrMagnitude;

                if (sqrMagnitude < closestDistanceSqr)
                {
                    closestDistanceSqr = sqrMagnitude;
                    view = starViews[i];
                }
            }
            view.InGameEvent = true;
            return view;
        }*/
    }
}