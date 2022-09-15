using FluentAssertions;
using PoolTools.Pool.API.Domain.Entities;
using PoolTools.Pool.API.Domain.Exceptions;
using PoolTools.Pool.API.Domain.ValueObjects;

namespace Domain.UnitTests.Entities;

public class PoolTests
{
    private readonly List<DraftPick> draftPicks = new() { DraftPick.From(1, 1), DraftPick.From(2, 1), DraftPick.From(3, 1), DraftPick.From(4, 1) };
    
    [Fact]
    public void ShouldAddNewTeamToPool()
    {
        var pool = new Pool("Test pool");

        
        var team = new PoolTeam("Test Team", "Owner 1", draftPicks);

        pool.AddTeam(team);

        pool.Teams.Count.Should().Be(1);
    }

    [Fact]
    public void ShouldThrowWhenAddingExistingTeamToPool()
    {
        var pool = new Pool { Name = "Test pool" };

        var team = new PoolTeam("Test Team", "Owner 1",draftPicks);

        pool.AddTeam(team);
        
        var act = () => pool.AddTeam(team);

        act.Should().Throw<DuplicatePoolTeamException>().WithMessage("Can't add team Test Team to pool Test pool. A team with the same name already exists.");
    }
}
