using System;
using UnityEngine;
using UnityEngine.UI;


namespace SpaceRogue.UI.Game
{
    public sealed class ObstacleUIEffectView : MonoBehaviour
    {

        #region Events

        public event Action VignetteSizeLimit;

        #endregion


        #region Fields

        private const string SIZE_PROPERTY = "_CircleSize";
        private const string TRANSITION_PROPERTY = "_CircleTransition";

        [SerializeField] private Image _image;
        [SerializeField, Tooltip("Seconds"), Min(0.0f)] private float _lerpDuration;
        [SerializeField, Range(0.0f, 1.0f)] private float _vignetteSize;
        [SerializeField, Range(0.0f, 1.0f)] private float _vignetteTransition;

        private Material _material;
        private float _timeElapsed;
        private bool _isChanged;

        #endregion


        #region Mono

        private void OnDisable() => _material.SetFloat(SIZE_PROPERTY, 0.0f);

        #endregion


        #region CodeLife

        public void SetVignetteSettings()
        {
            if (_image == null)
            {
                throw new Exception($"Missing Image component in {nameof(ObstacleUIEffectView)}");
            }

            _material = _image.material;
            _material.SetFloat(SIZE_PROPERTY, 0.0f);
            _material.SetFloat(TRANSITION_PROPERTY, _vignetteTransition);
        }

        #endregion


        #region Methods

        public void ChangeVignetteSize(bool isIncrease)
        {
            if (isIncrease != _isChanged)
            {
                _isChanged = isIncrease;
                _timeElapsed = 0.0f;
            }

            var startValue = _material.GetFloat(SIZE_PROPERTY);
            var endValue = isIncrease ? _vignetteSize : 0.0f;

            if (_timeElapsed < _lerpDuration)
            {
                _material.SetFloat(SIZE_PROPERTY, Mathf.Lerp(startValue, endValue, _timeElapsed / _lerpDuration));
                _timeElapsed += Time.deltaTime;
            }
            else
            {
                _material.SetFloat(SIZE_PROPERTY, endValue);
                _timeElapsed = 0.0f;
                VignetteSizeLimit?.Invoke();
            }
        }

        #endregion

    }
}