using SpaceRogue.Abstraction;
using Zenject;


namespace Gameplay.Movement
{
    public sealed class UnitTurningMouseFactory : PlaceholderFactory<EntityViewBase, IUnitTurningMouseInput, UnitMovementModel, UnitTurningMouse>
    {
    }
}