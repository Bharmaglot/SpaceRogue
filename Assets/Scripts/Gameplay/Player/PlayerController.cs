using Scriptables;
using System;
using Gameplay.Survival.Health;
using Gameplay.Survival.Shield;
using UnityEngine;
using Utilities.Reactive.SubscriptionProperty;
using Utilities.ResourceManagement;
using SpaceRogue.Abstraction;


namespace Gameplay.Player
{
    public sealed class PlayerController : BaseController //TODO Remove (legacy)
    {
        public PlayerView View => _view;

        private readonly ResourcePath _configPath = new(Constants.Configs.Player.PlayerConfig);
        private readonly ResourcePath _viewPath = new(Constants.Prefabs.Gameplay.Player);
        private readonly ResourcePath _crosshairPrefabPath = new(Constants.Prefabs.Stuff.Crosshair);

        private readonly PlayerConfig _config;
        private readonly PlayerView _view;

        private readonly SubscribedProperty<Vector3> _mousePositionInput = new();
        private readonly SubscribedProperty<float> _verticalInput = new();
        private readonly SubscribedProperty<bool> _primaryFireInput = new();
        private readonly SubscribedProperty<bool> _changeWeaponInput = new();

        public event Action PlayerDestroyed = () => { };
        public event Action OnControllerDispose = () => { };
        public SubscribedProperty<bool> NextLevelInput = new();
        public SubscribedProperty<bool> MapInput = new();

        public PlayerController(Vector3 playerPosition, HealthInfo healthInfo, ShieldInfo shieldInfo)
        {
            _config = ResourceLoader.LoadObject<PlayerConfig>(_configPath);
            _view = LoadView<PlayerView>(_viewPath, playerPosition);
        }


        public void ControllerDispose()
        {
            OnControllerDispose.Invoke();
            Dispose();
        }

    }
}