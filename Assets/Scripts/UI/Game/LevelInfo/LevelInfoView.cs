using UnityEngine;

namespace UI.Game.LevelInfo
{
    public sealed class LevelInfoView : MonoBehaviour
    {
        [field: SerializeField] public LevelNumberView LevelNumberView { get; private set; }
        [field: SerializeField] public EnemiesCountView EnemiesCountView { get; private set; }
        
        private const int Zero = 0;
        
        public void Init(int currentLevelNumber, int enemiesToWin)
        {
            LevelNumberView.InitNumber(currentLevelNumber.ToString());
            EnemiesCountView.Init(Zero, enemiesToWin);
        }

        public void Show()
        {
            LevelNumberView.Show();
            EnemiesCountView.Show();
        }

        public void Hide()
        {
            LevelNumberView.Hide();
            EnemiesCountView.Hide();
        }

        public void UpdateKillCounter(int defeatedEnemiesCount)
        {
            EnemiesCountView.EnemiesDestroyedCount.UpdateText(defeatedEnemiesCount.ToString());
        }
    }
}