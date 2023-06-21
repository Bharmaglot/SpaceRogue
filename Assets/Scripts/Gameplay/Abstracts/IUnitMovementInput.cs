using System;


namespace SpaceRogue.Abstraction
{
    public interface IUnitMovementInput
    {
        event Action<float> VerticalAxisInput;
    }
}