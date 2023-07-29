using Gameplay.Mechanics.Timer;
using Gameplay.Player;
using SpaceRogue.Gameplay.GameEvent.Factories;
using SpaceRogue.Scriptables.GameEvent;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Mathematics;
using Utilities.Unity;


namespace SpaceRogue.Gameplay.GameEvent.Comet
{
    public sealed class CometGameEvent : GameEvent
    {

        #region Fields

        private const int MAX_COUNT_OF_COMET_SPAWN_TRIES = 10;

        private readonly CometGameEventConfig _cometGameEventConfig;
        private readonly PlayerView _playerView;
        private readonly CometFactory _cometFactory;
        private readonly float _orthographicSize;

        private readonly List<Comet> _comets = new();

        #endregion


        #region CodeLife

        public CometGameEvent(CometGameEventConfig config, TimerFactory timerFactory, PlayerView playerView, CometFactory cometFactory) : base(config, timerFactory)
        {
            _cometGameEventConfig = config;
            _playerView = playerView;
            _cometFactory = cometFactory;
            _orthographicSize = Camera.main.orthographicSize;
        }

        #endregion


        #region Methods

        protected override void OnDispose()
        {
            foreach (var comet in _comets)
            {
                comet.Dispose();
            }
            _comets.Clear();
        }

        protected override bool RunGameEvent()
        {
            for (var i = 0; i < _comets.Count; i++)
            {
                if (_comets[i].IsDestroyed)
                {
                    _comets.Remove(_comets[i]);
                }
            }

            for (var i = 0; i < _cometGameEventConfig.CometConfig.CometCount; i++)
            {
                if (!TryGetNewCometPosition(out var position))
                {
                    continue;
                }
                var direction = _playerView.transform.position - position;
                var comet = _cometFactory.Create(_cometGameEventConfig.CometConfig, position, direction);
                AddGameEventIndicator(comet.CometView.Collider2D);
                _comets.Add(comet);
            }

            return _comets.Count != 0;
        }

        private bool TryGetNewCometPosition(out Vector3 position)
        {
            var tryCount = 0;
            var radius = _cometGameEventConfig.CometConfig.Size + _cometGameEventConfig.CometConfig.CheckRadius;
            do
            {
                position = GetRandomCometPosition();
                tryCount++;
            }
            while (UnityHelper.IsAnyObjectAtPosition(position, radius) && tryCount <= MAX_COUNT_OF_COMET_SPAWN_TRIES);

            return tryCount <= MAX_COUNT_OF_COMET_SPAWN_TRIES;
        }

        private Vector3 GetRandomCometPosition()
        {
            var angleDirection = RandomPicker.PickRandomAngle(360).normalized;
            var playerPosition = _playerView.transform.position;
            var offset = _orthographicSize * 2 + _cometGameEventConfig.CometConfig.Size + _cometGameEventConfig.CometConfig.Offset;
            var position = playerPosition + angleDirection * offset;
            return position;
        }

        #endregion

    }
}