using System;


namespace SpaceRogue.Abstraction
{
    public interface IChangeCharacterInput
    {
        event Action<bool> ChangeCharacterInput;
    }
}