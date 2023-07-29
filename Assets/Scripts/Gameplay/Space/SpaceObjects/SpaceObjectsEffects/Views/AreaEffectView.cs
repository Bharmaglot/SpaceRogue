using UI.Abstracts;
using UnityEngine;

namespace Gameplay.Space.SpaceObjects.SpaceObjectsEffects.Views
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class AreaEffectView : MonoBehaviour, IShowableView, IHideableView
    {
        public void Hide() => gameObject.SetActive(false);
        public void Show() => gameObject.SetActive(true);
    }
}