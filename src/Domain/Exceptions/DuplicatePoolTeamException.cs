namespace PoolTools.Pool.API.Domain.Exceptions;

public class DuplicatePoolTeamException : Exception
{
    public DuplicatePoolTeamException(string poolName, PoolTeam team) 
        : base($"Can't add team {team.Name} to pool {poolName}. A team with the same name already exists.")
    {
    }
}
