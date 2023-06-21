using System;


namespace SpaceRogue.Abstraction
{
    public interface IUnitMovementInput
    {
        event Action<float> VerticalAxisInput;
        event Action<float> HorizontalAxisInput;
    }
}