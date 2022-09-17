namespace PoolTools.Pool.API.Domain.Exceptions;

public sealed class UnsupportedPlayerReleaseException : Exception
{
    public UnsupportedPlayerReleaseException(int teamId, Player releasedPlayer)
        : base($"Can't release player with id {releasedPlayer.Id} from team {teamId} since it's not part of the team roster.")
    {
    }
}
