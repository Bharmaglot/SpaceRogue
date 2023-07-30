using UI.Abstracts;
using UI.Common;
using UnityEngine;
using UnityEngine.UI;


namespace SpaceRogue.UI.Game
{
    public sealed class PlayerUsedItemView : MonoBehaviour, IShowableView, IHideableView
    {
        [field: SerializeField] public Image Panel { get; private set; }
        [field: SerializeField] public Color ColorActive { get; private set; }
        [field: SerializeField] public Color ColorNotActive { get; private set; }
        [field: SerializeField] public TextView ItemTextView { get; private set; }

        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);

        public void Init(string text) => ItemTextView.Init(text);
        public void UpdateText(string text) => ItemTextView.UpdateText(text);
    }
}