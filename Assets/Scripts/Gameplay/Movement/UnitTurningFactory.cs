using SpaceRogue.Abstraction;
using Zenject;

namespace Gameplay.Movement
{
    public sealed class UnitTurningFactory : PlaceholderFactory<EntityViewBase, IUnitTurningInput, UnitMovementModel, UnitTurning>
    {
    }
}