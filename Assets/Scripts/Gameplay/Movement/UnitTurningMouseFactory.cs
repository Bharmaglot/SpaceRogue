using SpaceRogue.Abstraction;
using Zenject;


namespace Gameplay.Movement
{
    public sealed class UnitTurningMouseFactory : PlaceholderFactory<EntityView, IUnitTurningMouseInput, UnitMovementModel, UnitTurningMouse>
    {
    }
}