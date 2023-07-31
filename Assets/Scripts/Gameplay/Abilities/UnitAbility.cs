using SpaceRogue.Abstraction;
using System;


namespace SpaceRogue.Gameplay.Abilities
{
    public sealed class UnitAbility : IDisposable
    {

        #region Fields

        private readonly IUnitAbilityInput _input;

        #endregion


        #region Properties

        public bool IsEnable { get; set; }

        public Ability Ability { get; private set; }

        #endregion


        #region CodeLife

        public UnitAbility(Ability ability, IUnitAbilityInput input)
        {
            Ability = ability;
            _input = input;

            _input.AbilityInput += AbilityInput;
        }

        public void Dispose()
        {
            _input.AbilityInput -= AbilityInput;

            Ability.Dispose();
        }

        #endregion


        #region Methods

        private void AbilityInput(bool buttonIsPressed)
        {
            if (buttonIsPressed && IsEnable)
            {
                Ability.UseAbility();
            }
        }

        #endregion

    }
}