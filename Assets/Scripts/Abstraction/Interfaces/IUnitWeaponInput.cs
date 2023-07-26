using System;


namespace SpaceRogue.Abstraction
{
    public interface IUnitWeaponInput
    {
        event Action<bool> PrimaryFireInput;
        event Action<bool> ChangeWeaponInput;
        event Action<bool> AbilityInput;
    }
}