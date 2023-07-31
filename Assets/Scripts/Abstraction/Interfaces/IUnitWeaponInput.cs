using System;


namespace SpaceRogue.Abstraction
{
    public interface IUnitWeaponInput
    {
        event Action<bool> PrimaryFireInput;
    }
}