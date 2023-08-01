using Gameplay.Survival;
using SpaceRogue.Abstraction;
using SpaceRogue.Gameplay.Abilities;
using SpaceRogue.Gameplay.Shooting;
using SpaceRogue.InputSystem;
using SpaceRogue.Player.Movement;
using System;
using System.Collections.Generic;
using Zenject;


namespace SpaceRogue.Gameplay.Player.Character
{
    public sealed class CharacterFactory : PlaceholderFactory<EntityViewBase, UnitMovement, List<Character>>
    {

        #region Events

        public event Action<List<Character>> OnCharactersCreated;

        #endregion


        #region Fields

        private readonly PlayerInput _playerInput;
        private readonly EntitySurvivalFactory _entitySurvivalFactory;
        private readonly UnitWeaponFactory _unitWeaponFactory;
        private readonly UnitAbilityFactory _unitAbilityFactory;
        private readonly List<CharacterConfig> _characterConfigs;

        #endregion


        #region CodeLife

        public CharacterFactory(
            PlayerInput playerInput,
            EntitySurvivalFactory entitySurvivalFactory,
            UnitWeaponFactory unitWeaponFactory,
            UnitAbilityFactory unitAbilityFactory,
            List<CharacterConfig> characterConfigs)
        {
            _playerInput = playerInput;
            _entitySurvivalFactory = entitySurvivalFactory;
            _unitWeaponFactory = unitWeaponFactory;
            _unitAbilityFactory = unitAbilityFactory;
            _characterConfigs = characterConfigs;
        }

        #endregion


        #region Methods

        public override List<Character> Create(EntityViewBase entityView, UnitMovement unitMovement)
        {
            var result = new List<Character>();

            foreach (var characterConfig in _characterConfigs)
            {
                var characterSurvival = _entitySurvivalFactory.Create(entityView, characterConfig.Survival);
                var unitWeapon = _unitWeaponFactory.Create(entityView, characterConfig.MountedWeapon, unitMovement, _playerInput);
                var unitAbility = _unitAbilityFactory.Create(entityView, characterConfig.Ability, unitMovement, _playerInput);
                var character = new Character(characterConfig.Name, characterConfig.CharacterIcon, characterConfig.SpaceshipSprite, characterSurvival, unitWeapon, unitAbility);
                character.SetCharacterActive(false);
                result.Add(character);
            }

            result[0].SetCharacterActive(true);
            OnCharactersCreated?.Invoke(result);
            return result;
        }

        #endregion

    }
}