using SpaceRogue.Abstraction;
using Zenject;


namespace Gameplay.Movement
{
    public sealed class UnitMovementFactory : PlaceholderFactory<EntityView, IUnitMovementInput, UnitMovementModel, UnitMovement>
    {
    }
}