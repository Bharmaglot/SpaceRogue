using System;
using UnityEngine;


namespace SpaceRogue.Abstraction
{
    public interface IUnitTurningMouseInput
    {
        event Action<Vector3> MousePositionInput;
    }
}