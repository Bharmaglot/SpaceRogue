using UI.Abstracts;
using UI.Common;
using UnityEngine;

namespace UI.Game
{
    public sealed class LevelNumberView : MonoBehaviour, IShowableView, IHideableView
    {
        [field: SerializeField] public TextView LevelNumber { get; private set; }

        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);

        public void InitNumber(string number) => LevelNumber.Init(number);
        public void UpdateNumber(string number) => LevelNumber.UpdateText(number);
    }
}