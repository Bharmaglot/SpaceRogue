using UI.Abstracts;
using UI.Common;
using UnityEngine;
using UnityEngine.UI;


namespace SpaceRogue.UI.Game
{
    public sealed class PlayerUsedItemView : MonoBehaviour, IShowableView, IHideableView
    {

        #region Fields

        [SerializeField] private Image _panel;
        [SerializeField] private Color _colorActive;
        [SerializeField] private Color _colorNotActive;

        #endregion


        #region Properties

        [field: SerializeField] public TextView ItemTextView { get; private set; }

        #endregion


        #region CodeLife

        public void Init(string text) => ItemTextView.Init(text); 

        #endregion


        #region Methods

        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);

        public void UpdateText(string text) => ItemTextView.UpdateText(text);

        public void SetPanelActive(bool isActive) => _panel.color = isActive ? _colorActive : _colorNotActive;

        #endregion

    }
}