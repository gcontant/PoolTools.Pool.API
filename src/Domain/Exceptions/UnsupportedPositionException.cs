namespace PoolTools.Pool.API.Domain.Exceptions;

public class UnsupportedPositionException : Exception
{
    public UnsupportedPositionException(string code)
        : base($"Position \"{code}\" is unsupported.")
    {
    }
}
