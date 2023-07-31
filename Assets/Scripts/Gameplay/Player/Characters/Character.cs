using Gameplay.Survival;
using SpaceRogue.Gameplay.Abilities;
using SpaceRogue.Gameplay.Shooting;
using System;
using UnityEngine;

namespace SpaceRogue.Gameplay.Player.Character
{
    public sealed class Character : IDisposable
    {

        #region Events

        public event Action<Character> CharacterDestroyed;

        #endregion


        #region Properties

        public Sprite CharacterIcon { get; private set; }
        public Sprite SpaceshipSprite { get; private set; }
        public EntitySurvival Survival { get; private set; }
        public UnitWeapon UnitWeapon { get; private set; }
        public UnitAbility UnitAbility { get; private set; }

        #endregion


        #region CodeLife

        public Character(
            Sprite characterIcon,
            Sprite spaceshipSprite,
            EntitySurvival characterSurvival,
            UnitWeapon unitWeapon,
            UnitAbility unitAbility)
        {
            CharacterIcon = characterIcon;
            SpaceshipSprite = spaceshipSprite;
            Survival = characterSurvival;
            UnitWeapon = unitWeapon;
            UnitAbility = unitAbility;

            Survival.UnitDestroyed += OnDeath;
        }

        public void Dispose()
        {
            Survival.UnitDestroyed -= OnDeath;
            Survival.Dispose();

            UnitWeapon.Dispose();
            UnitAbility.Dispose();
        }

        #endregion


        #region Metods

        public void SetCharacterActive(bool isActive)
        {
            Survival.CanReceiveDamage = isActive;
            UnitWeapon.IsEnable = isActive;
            UnitAbility.IsEnable = isActive;
        }

        private void OnDeath() => CharacterDestroyed?.Invoke(this);

        #endregion

    }
}