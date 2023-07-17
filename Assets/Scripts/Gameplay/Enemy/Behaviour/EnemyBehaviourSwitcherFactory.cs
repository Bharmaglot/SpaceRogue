using Gameplay.Movement;
using SpaceRogue.Enemy.Movement;
using Zenject;


namespace Gameplay.Enemy.Behaviour
{
    public sealed class EnemyBehaviourSwitcherFactory : PlaceholderFactory<EnemyView, EnemyInput, UnitMovementModel, EnemyBehaviourConfig, EnemyBehaviourSwitcher>
    {
    }
}