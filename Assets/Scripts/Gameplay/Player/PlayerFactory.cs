using Gameplay.Movement;
using SpaceRogue.Gameplay.Events;
using SpaceRogue.InputSystem;
using System;
using Zenject;


namespace SpaceRogue.Gameplay.Player
{
    public sealed class PlayerFactory : PlaceholderFactory<Player>
    {

        #region Events

        public event Action<PlayerSpawnedEventArgs> PlayerSpawned;

        public event Action<Player> OnPlayerSpawned;

        #endregion


        #region Fields

        private readonly PlayerViewFactory _playerViewFactory;
        private readonly PlayerInput _playerInput;
        private readonly UnitMovementConfig _unitMovementConfig;
        private readonly UnitMovementModelFactory _unitMovementModelFactory;
        private readonly PlayerMovementFactory _playerMovementFactory;
        private readonly UnitTurningMouseFactory _unitTurningMouseFactory;
        private readonly PlayerSurvivalFactory _playerSurvivalFactory;
        private readonly PlayerWeaponFactory _playerWeaponsFactory;

        #endregion


        #region CodeLife

        public PlayerFactory(
            PlayerViewFactory playerViewFactory,
            PlayerInput playerInput,
            UnitMovementConfig unitMovementConfig,
            UnitMovementModelFactory unitMovementModelFactory,
            PlayerMovementFactory playerMovementFactory,
            UnitTurningMouseFactory unitTurningMouseFactory,
            PlayerSurvivalFactory playerSurvivalFactory,
            PlayerWeaponFactory playerWeaponsFactory)
        {
            _playerViewFactory = playerViewFactory;
            _playerInput = playerInput;
            _unitMovementConfig = unitMovementConfig;
            _unitMovementModelFactory = unitMovementModelFactory;
            _playerMovementFactory = playerMovementFactory;
            _unitTurningMouseFactory = unitTurningMouseFactory;
            _playerSurvivalFactory = playerSurvivalFactory;
            _playerWeaponsFactory = playerWeaponsFactory;
        }

        #endregion


        #region Methods

        public override Player Create()
        {
            var playerView = _playerViewFactory.Create();
            var model = _unitMovementModelFactory.Create(_unitMovementConfig);
            var unitMovement = _playerMovementFactory.Create(playerView, _playerInput, model);
            var unitTurningMouse = _unitTurningMouseFactory.Create(playerView, _playerInput, model);
            var unitWeapons = _playerWeaponsFactory.Create(playerView, unitMovement);
            var playerSurvival = _playerSurvivalFactory.Create(playerView);

            var player = new Player(playerView, unitMovement, unitTurningMouse, playerSurvival, unitWeapons, _playerInput);

            PlayerSpawned?.Invoke(new PlayerSpawnedEventArgs(player, playerView.transform));
            OnPlayerSpawned?.Invoke(player);

            return player;
        }

        #endregion

    }
}