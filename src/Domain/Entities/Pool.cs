namespace PoolTools.Pool.API.Domain.Entities;

public class Pool : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public IList<PoolTeam> Teams { get; private set; } = new List<PoolTeam>();

    public Pool(string name)
    {
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
