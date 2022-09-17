namespace PoolTools.Pool.API.Domain.Entities;

public class PoolOptions : BaseEntity
{
    public int PoolId { get; set; }
    public Pool Pool { get; set; }

    public decimal MaximumCap { get; set; }

    public int RosterSize { get; set; }
    public int RequiredForwards { get; set; }
    public int RequiredDefencemen { get; set; }
    public int RequiredGoaltenders { get; set; }
}
