using Gameplay.Movement;
using Gameplay.Survival;
using SpaceRogue.Abstraction;
using SpaceRogue.Gameplay.Player;
using SpaceRogue.Gameplay.Shooting;
using SpaceRogue.Gameplay.Shooting.Scriptables;
using SpaceRogue.Player.Movement;
using SpaceRogue.Scriptables;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


namespace SpaceRogue.Gameplay.Installers
{
    public sealed class PlayerInstaller : MonoInstaller
    {

        #region Properties

        [field: SerializeField] public PlayerView PlayerViewPrefab { get; private set; }
        [field: SerializeField] public PlayerConfig PlayerConfig { get; private set; }

        #endregion


        #region Methods

        public override void InstallBindings()
        {
            InstallPlayerView();
            InstallPlayerMovement();
            InstallPlayerHealth();
            InstallPlayerWeapon();
            InstallPlayer();
        }

        private void InstallPlayerView()
        {
            Container
                .Bind<PlayerView>()
                .FromInstance(PlayerViewPrefab)
                .WhenInjectedInto<PlayerViewFactory>();

            Container
                .BindFactory<PlayerView, PlayerViewFactory>()
                .AsSingle();
        }

        private void InstallPlayerMovement()
        {
            Container
                .Bind<UnitMovementConfig>()
                .FromInstance(PlayerConfig.UnitMovement)
                .WhenInjectedInto<PlayerFactory>();

            Container
                .BindFactory<PlayerView, IUnitMovementInput, UnitMovementModel, UnitMovement, PlayerMovementFactory>()
                .AsSingle();
        }

        private void InstallPlayerHealth()
        {
            Container.Bind<EntitySurvivalConfig>()
                .FromInstance(PlayerConfig.Survival)
                .WhenInjectedInto<PlayerSurvivalFactory>();

            Container
                .BindFactory<EntityViewBase, EntitySurvival, PlayerSurvivalFactory>()
                .AsSingle();
        }

        private void InstallPlayerWeapon()
        {
            Container
                .Bind<List<MountedWeaponConfig>>()
                .FromInstance(PlayerConfig.AvailableWeapons)
                .WhenInjectedInto<PlayerWeaponFactory>();

            Container
                .BindFactory<PlayerView, UnitMovement, List<UnitWeapon>, PlayerWeaponFactory>()
                .AsSingle();
        }

        private void InstallPlayer()
        {
            Container
                .BindFactory<Player.Player, PlayerFactory>()
                .AsSingle();
        }

        #endregion

    }
}