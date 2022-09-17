namespace PoolTools.Pool.API.Domain.Exceptions;

public sealed class UnsupportedPlayerDraftException : Exception
{
    public UnsupportedPlayerDraftException(int teamId, Player releasedPlayer)
        : base($"Can't draft player with id {releasedPlayer.Id} from team {teamId} since it's already part of the team roster.")
    {
    }
}
