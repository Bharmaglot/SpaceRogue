using UI.Abstracts;
using UI.Common;
using UnityEngine;
using UnityEngine.UI;


namespace SpaceRogue.UI.Game
{
    public sealed class PlayerUsedItemView : MonoBehaviour, IShowableView, IHideableView
    {
        [field: SerializeField] private Image _panel;
        [field: SerializeField] private Color _colorActive;
        [field: SerializeField] private Color _colorNotActive;
        [field: SerializeField] public TextView ItemTextView { get; private set; }

        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);

        public void Init(string text) => ItemTextView.Init(text);
        public void UpdateText(string text) => ItemTextView.UpdateText(text);

        public void SetPanelActive(bool isActive) => _panel.color = isActive ? _colorActive : _colorNotActive;
    }
}