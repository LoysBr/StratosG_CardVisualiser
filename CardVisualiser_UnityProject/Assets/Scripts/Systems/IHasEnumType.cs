namespace StratosphereGames.Base
{
    public interface IHasEnumType<TEnum>
    {
        TEnum Type { get; }
    }
}