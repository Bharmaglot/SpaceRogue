using Gameplay.Movement;
using SpaceRogue.Abstraction;
using SpaceRogue.Gameplay.Abilities;
using SpaceRogue.Gameplay.Abilities.Scriptables;
using SpaceRogue.Gameplay.Player;
using SpaceRogue.Gameplay.Player.Character;
using SpaceRogue.Gameplay.Pooling;
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
        [field: SerializeField] public AbilityPool AbilityPool { get; private set; }

        #endregion


        #region Methods

        public override void InstallBindings()
        {
            InstallPlayerView();
            InstallPlayerMovement();
            InstallCharacters();
            InstallPlayer();
            InstallAbilityFactories();
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

        private void InstallCharacters()
        {
            Container
                .Bind<List<CharacterConfig>>()
                .FromInstance(PlayerConfig.Characters)
                .WhenInjectedInto<CharacterFactory>();

            Container
                .BindFactory<EntityViewBase, UnitMovement, List<Character>, CharacterFactory>()
                .AsSingle();
        }

        private void InstallPlayer()
        {
            Container
                .BindFactory<Player.Player, PlayerFactory>()
                .AsSingle();
        }

        private void InstallAbilityFactories()
        {

            Container
                .Bind<AbilityPool>()
                .FromInstance(AbilityPool)
                .AsSingle();

            Container
                .BindIFactory<AbilityConfig, EntityViewBase, UnitMovement, Ability>()
                .FromFactory<AbilityFactory>();

            Container
                .Bind<AbilityFactory>()
                .AsCached();

            Container
                .BindFactory<Vector2, AbilityConfig, AbilityView, AbilityViewFactory>()
                .AsSingle();

            Container
                .BindFactory<AbilityView, Transform, ShotgunAbilityConfig, IDestroyable, GravitationMine, GravitationMineFactory>()
                .AsSingle();

            Container
                .BindFactory<EntityViewBase, AbilityConfig, UnitMovement, IUnitAbilityInput, UnitAbility, UnitAbilityFactory>()
                .AsSingle();
        }

        #endregion

    }
}