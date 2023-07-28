using System;
using UI.Abstracts;
using UI.Common;
using UnityEngine;


namespace SpaceRogue.UI.Game.LevelFinishedPopup
{
    public sealed class NextLevelMessageView : MonoBehaviour, IShowableView, IHideableView
    {

        #region Properties

        [field: SerializeField] public TextView LevelsNumber { get; private set; }
        [field: SerializeField] public ButtonView NextLevelButton { get; private set; }

        #endregion


        #region CodeLife

        public void Init(string levelNumber, Action onClickAction)
        {
            LevelsNumber.Init(levelNumber);
            NextLevelButton.Init(onClickAction);
        }

        #endregion


        #region Methods

        public void Show() => gameObject.SetActive(true);

        public void Hide() => gameObject.SetActive(false);

        #endregion

    }
}
