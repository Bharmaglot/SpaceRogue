using System;


namespace SpaceRogue.Abstraction
{
    public interface IUnitWeaponInput
    {
        event Action<bool> PrimaryFireInput;
        event Action<bool> AbilityInput;
        
        /// <summary>
        /// Invoked when weapon need changed. True - next weapon. False - previous weapon
        /// </summary>
        event Action<bool> ChangeWeaponInput;
    }
}