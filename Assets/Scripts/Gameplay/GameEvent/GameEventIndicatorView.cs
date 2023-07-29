using UI.Abstracts;
using UnityEngine;
using UnityEngine.UI;


namespace SpaceRogue.Gameplay.GameEvent
{
    public sealed class GameEventIndicatorView : MonoBehaviour, IShowableView, IHideableView
    {
        [field: SerializeField] public Image Icon { get; private set; }
        [field: SerializeField] public RectTransform IndicatorDiameter { get; private set; }

        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);
    } 
}
