using System;


namespace SpaceRogue.Abstraction
{
    public interface IDestroyable
    {
        event Action Destroyed;
    }
}