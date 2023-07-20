using System;
using Gameplay.Movement;
using Gameplay.Shooting;
using Gameplay.Survival;
using SpaceRogue.Player.Movement;


namespace Gameplay.Player
{
    public sealed class Player : IDisposable
    {
        private readonly UnitMovement _unitMovement;
        private readonly UnitTurningMouse _unitTurningMouse;
        private readonly UnitWeapon _unitWeapon;

        public event Action PlayerDestroyed;
        public event Action PlayerDisposed;

        public PlayerView PlayerView { get; }
        public EntitySurvival Survival { get; }

        private bool _disposing;

        public Player(
            PlayerView playerView, 
            UnitMovement unitMovement, 
            UnitTurningMouse unitTurningMouse,
            EntitySurvival playerSurvival,
            UnitWeapon unitWeapon)
        {
            PlayerView = playerView;
            _unitMovement = unitMovement;
            _unitTurningMouse = unitTurningMouse;
            _unitWeapon = unitWeapon;
            Survival = playerSurvival;

            Survival.UnitDestroyed += OnDeath;
        }

        public void Dispose()
        {
            if (_disposing) return;
            _disposing = true;
            Survival.UnitDestroyed -= OnDeath;
            
            PlayerDisposed?.Invoke();

            Survival.Dispose();
            _unitMovement.Dispose();
            _unitTurningMouse.Dispose();
            _unitWeapon.Dispose();

            if (PlayerView is not null)
            {
                UnityEngine.Object.Destroy(PlayerView.gameObject);
            }
        }

        private void OnDeath()
        {
            PlayerDestroyed?.Invoke();
            Dispose();
        }
    }
}