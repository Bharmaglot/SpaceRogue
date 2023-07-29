using UI.Common;
using UnityEngine;

namespace UI.Game
{
    public sealed class EnemiesCountView : MonoBehaviour
    {
        [field: SerializeField] public TextView EnemiesDestroyedCount { get; private set; }
        [field: SerializeField] public TextView EnemiesCount { get; private set; }

        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);

        public void Init(string enemiesDestroyedCount, string enemiesCount)
        {
            EnemiesDestroyedCount.Init(enemiesDestroyedCount);
            EnemiesCount.Init(enemiesCount);
        }

        public void UpdateCounter(string enemiesDestroyedCount)
        {
            EnemiesDestroyedCount.UpdateText(enemiesDestroyedCount);
        }
    }
}