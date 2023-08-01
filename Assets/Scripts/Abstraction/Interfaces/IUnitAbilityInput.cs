using System;


namespace SpaceRogue.Abstraction
{
    public interface IUnitAbilityInput
    {
        event Action<bool> AbilityInput;
    }
}