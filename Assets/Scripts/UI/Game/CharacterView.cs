using UI.Abstracts;
using UI.Common;
using UnityEngine;
using UnityEngine.UI;


namespace SpaceRogue.UI.Game
{
    public sealed class CharacterView : MonoBehaviour, IShowableView, IHideableView
    {

        #region Fields

        [field: SerializeField] private Image _panel;
        [field: SerializeField] private Color _colorActive;
        [field: SerializeField] private Color _colorNotActive;

        #endregion


        #region Properties

        [field: SerializeField] public Image Icon { get; private set; }
        [field: SerializeField] public TextView NameTextView { get; private set; }
        [field: SerializeField] public PlayerStatusBarView PlayerStatusBarView { get; private set; }

        #endregion


        #region CodeLife

        public void Init(string text, Sprite sprite)
        {
            NameTextView.Init(text);
            Icon.sprite = sprite;
        } 
        
        #endregion


        #region Methods

        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);

        public void SetPanelActive(bool isActive)
        {
            _panel.color = isActive ? _colorActive : _colorNotActive;

            if (isActive)
            {
                _panel.color = _colorActive;
                PlayerStatusBarView.Hide();
            }
            else
            {
                _panel.color = _colorNotActive;
                PlayerStatusBarView.Show();
            }
        }

        #endregion

    }
}