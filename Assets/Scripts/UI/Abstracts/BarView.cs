using SpaceRogue.Abstraction;
using UnityEngine;


namespace UI.Abstracts
{
    public abstract class BarView : MonoBehaviour, IBarView
    {
        public abstract void Init(float minValue, float maxValue, float currentValue);
        public abstract void UpdateValue(float newValue);
    }
}