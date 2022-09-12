namespace PoolTools.Pool.API.Domain.Entities;

public record PoolId(int Value) : StronglyTypedId<int>(Value);

public class Pool : BaseEntity<PoolId, int>
{
    public string Name { get; set; } = string.Empty;
    public IList<PoolTeam> Teams { get; set; } = new List<PoolTeam>();

    public Pool(PoolId id,string name)
    {
        Id = id;
        Name = name;
    }

    // Empty constructor for EF
    public Pool()
    {

    }

    public void AddTeam(PoolTeam newTeam)
    {
        // Put in validation?
        if (Teams.Contains(newTeam))
        {
            throw new DuplicatePoolTeamException(Name, newTeam);
        }

        Teams.Add(newTeam);
    }
}
