using SpaceRogue.Abstraction;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


namespace UI.Common
{
    [RequireComponent(typeof(Button))]
    public sealed class ButtonView : MonoBehaviour, IButtonView
    {
        private Button _button;

        public void Init(Action onClickAction)
        {
            _button = GetComponent<Button>();
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(new UnityAction(onClickAction));
        }

        public void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}