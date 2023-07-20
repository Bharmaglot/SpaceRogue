using System;
using UI.Abstracts;
using UI.Common;
using UnityEngine;

namespace UI.Game.PlayerDestroyedPopup
{
    public sealed class DestroyPlayerMessageView : MonoBehaviour, IShowableView, IHideableView
    {
        [field: SerializeField] public TextView LevelsNumber { get; private set; }
        [field: SerializeField] public ButtonView DestroyPlayerButton { get; private set; }

        public void Init(string levelsNumber, Action onClickAction)
        {
            LevelsNumber.Init(levelsNumber);
            DestroyPlayerButton.Init(onClickAction);
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
