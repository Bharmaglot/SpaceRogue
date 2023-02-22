namespace Gameplay.Damage
{
    public interface IRepeatableDamageView : IDamagingView
    {
        public float DamageCooldown { get; set; }
    }
}