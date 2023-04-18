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

        public void Init(int enemiesDestroyedCount, int enemiesCount)
        {
            EnemiesDestroyedCount.Init(enemiesDestroyedCount.ToString());
            EnemiesCount.Init(enemiesCount.ToString());
        }

        public void UpdateCounter(float enemiesDestroyedCount)
        {
            EnemiesDestroyedCount.UpdateText(enemiesDestroyedCount.ToString());
        }
    }
}