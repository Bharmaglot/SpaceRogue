namespace SpaceRogue.Abstraction
{
    public interface IBarView
    {
        void Init(float minValue, float maxValue, float currentValue);
        void UpdateValue(float newValue);
    }
}