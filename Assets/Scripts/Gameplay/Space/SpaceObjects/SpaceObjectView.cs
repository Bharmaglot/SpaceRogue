using UnityEngine;


namespace Gameplay.Space.SpaceObjects
{
    [RequireComponent(typeof(CircleCollider2D))]
    public sealed class SpaceObjectView : MonoBehaviour
    {

        #region Properties

        [field: SerializeField] public CircleCollider2D CircleCollider2D { get; private set; }
        [field: SerializeField] public SpriteRenderer SpriteRenderer { get; private set; }
        [field: SerializeField] public SpriteRenderer MinimapIconSpriteRenderer { get; private set; }

        [field: SerializeField] public bool IsVisibleForGameEvents { get; private set; } = true;

        public bool InGameEvent { get; private set; }

        #endregion


        #region Mono
        
        private void OnValidate()
        {
            CircleCollider2D ??= GetComponent<CircleCollider2D>();
        } 

        #endregion


        #region Methods

        public void MarkInGameEvent() => InGameEvent = true;

        #endregion

    }
}
