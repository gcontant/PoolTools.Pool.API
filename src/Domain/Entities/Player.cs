using System.ComponentModel.DataAnnotations;

namespace PoolTools.Pool.API.Domain.Entities;

public record PlayerId(int Value) : StronglyTypedId<int>(Value);
public class Player : BaseEntity<PlayerId, int>
{
    public string FullName { get; set; } = string.Empty;
    public Position Position { get; set; } = default!;
    public Team Team { get; set; } = default!;

    // Empty constructor for EF
    public Player()
    {

    }
    public Player(string name, Position position, Team team)
    {
        FullName = name;
        Position = position;
        Team = team;
    }



}