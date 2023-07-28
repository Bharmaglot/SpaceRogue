using System;
using UI.Abstracts;
using UI.Common;
using UnityEngine;


namespace SpaceRogue.UI.Game.PlayerDestroyedPopup
{
    public sealed class DestroyPlayerMessageView : MonoBehaviour, IShowableView, IHideableView
    {

        #region Properties

        [field: SerializeField] public TextView LevelsNumber { get; private set; }
        [field: SerializeField] public ButtonView DestroyPlayerButton { get; private set; }

        #endregion


        #region CodeLife

        public void Init(string levelsNumber, Action onClickAction)
        {
            LevelsNumber.Init(levelsNumber);
            DestroyPlayerButton.Init(onClickAction);
        }

        #endregion
        
        
        #region Methods

        public void Show() => gameObject.SetActive(true);

        public void Hide() => gameObject.SetActive(false);

        #endregion

    }
}
