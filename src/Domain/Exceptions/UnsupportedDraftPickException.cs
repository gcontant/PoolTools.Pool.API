namespace PoolTools.Pool.API.Domain.Exceptions;

public class UnsupportedDraftPickException : Exception
{
    public UnsupportedDraftPickException(int round, int position) : base($"Draft pick for round {round} and position {position} is unsupported.")
    {
    }
}
