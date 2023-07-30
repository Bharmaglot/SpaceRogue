using UI.Abstracts;
using UnityEngine;
using UnityEngine.UI;


namespace SpaceRogue.UI.Game
{
    public sealed class CharacterView : MonoBehaviour, IShowableView, IHideableView
    {
        [field: SerializeField] public Image Image { get; private set; }

        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);
    }
}