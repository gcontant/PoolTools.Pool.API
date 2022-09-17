using System.ComponentModel.DataAnnotations;

namespace PoolTools.Pool.API.Domain.Entities;

public class Player : BaseEntity
{
    public string FullName { get; set; } = string.Empty;
    public Position Position { get; set; } = default!;
    public Team Team { get; set; } = default!;
    public decimal AAV { get; set; }

    // Empty constructor for EF
    public Player()
    {

    }
    public Player(string name, Position position, Team team, decimal aav)
    {
        FullName = name;
        Position = position;
        Team = team;
        AAV = aav;
    }



}