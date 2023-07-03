namespace SpaceRogue.Abstraction
{
    public interface IIdentityItem<out TType>
    {
        TType Id { get; }
    }
}