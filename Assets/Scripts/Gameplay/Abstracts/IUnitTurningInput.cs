using System;


namespace SpaceRogue.Abstraction
{
    public interface IUnitTurningInput
    {
        event Action<float> HorizontalAxisInput;
    }
}