using UI.Abstracts;
using UI.Game;
using UnityEngine;


namespace SpaceRogue.UI.Game.LevelInfo
{
    public sealed class LevelInfoView : MonoBehaviour, IShowableView, IHideableView
    {

        #region Properties

        [field: SerializeField] public LevelNumberView LevelNumberView { get; private set; }
        [field: SerializeField] public EnemiesCountView EnemiesCountView { get; private set; }

        #endregion


        #region CodeLife

        public void Init(string currentLevelNumber, string enemiesKilled, string enemiesToWin)
        {
            LevelNumberView.InitNumber(currentLevelNumber);
            EnemiesCountView.Init(enemiesKilled, enemiesToWin);
        }

        #endregion


        #region Methods

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

        public void UpdateLevelNumber(string levelNumber) => LevelNumberView.UpdateNumber(levelNumber);

        public void UpdateKillCounter(string defeatedEnemiesCount) => EnemiesCountView.EnemiesDestroyedCount.UpdateText(defeatedEnemiesCount);

        #endregion

    }
}