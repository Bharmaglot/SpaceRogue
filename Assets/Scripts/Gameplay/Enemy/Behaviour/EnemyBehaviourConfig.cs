using UnityEngine;

namespace Gameplay.Enemy.Behaviour
{
    [CreateAssetMenu(fileName = nameof(EnemyBehaviourConfig), menuName = "Configs/Enemy/" + nameof(EnemyBehaviourConfig))]
    public sealed class EnemyBehaviourConfig : ScriptableObject
    {
        [field: SerializeField] public EnemyState StartEnemyState { get; private set; } = EnemyState.PassiveRoaming;
        [field: SerializeField] public float PlayerDetectionRadius { get; private set; }
        [field: SerializeField] public float CallToArmsRadius { get; private set; }
        [field: SerializeField] public float ApproachDistance { get; private set; }
        [field: SerializeField, Min(1)] public float FiringAngle { get; private set; }

        [field: Header("Check Obstacles")]
        [field: SerializeField, Min(1)] public float FrontCheckDistance { get; private set; } = 30;
        [field: SerializeField, Min(1)] public float SideCheckDistance { get; private set; } = 5;
    }
}