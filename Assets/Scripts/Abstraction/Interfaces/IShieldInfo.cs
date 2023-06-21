namespace SpaceRogue.Abstraction
{
    public interface IShieldInfo
    {
        float MaximumShield { get; }
        float StartingShield { get; }
        float Cooldown { get; }
    }
}