using System;
using UI.Abstracts;
using UI.Common;
using UnityEngine;

namespace UI.Game.LevelFinishedPopup
{
    public sealed class NextLevelMessageView : MonoBehaviour, IShowableView, IHideableView
    {
        [field: SerializeField] public TextView LevelsNumber { get; private set; }
        [field: SerializeField] public ButtonView NextLevelButton { get; private set; }

        public void Init(string levelNumber, Action onClickAction)
        {
            LevelsNumber.Init(levelNumber);
            NextLevelButton.Init(onClickAction);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    } 
}
