using SpaceRogue.Abstraction;
using SpaceRogue.Player.Movement;
using Zenject;


namespace Gameplay.Movement
{
    public sealed class UnitMovementFactory : PlaceholderFactory<EntityViewBase, IUnitMovementInput, UnitMovementModel, UnitMovement>
    {
    }
}