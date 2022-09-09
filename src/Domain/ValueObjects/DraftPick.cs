namespace PoolTools.Pool.API.Domain.ValueObjects;

public class DraftPick : ValueObject
{
    public int Round { get; set; }
    public int Position { get; set; }

    static DraftPick()
    { }

    private DraftPick()
    { }

    private DraftPick(int round, int position)
    {
        Round = round;
        Position = position;
    }

    public static DraftPick From(int round, int position)
    {
        if (round > 5 || position > 12)
        {
            throw new UnsupportedDraftPickException(round, position);
        }

        return new DraftPick(round,position);
    }

    public override string ToString()
    {
        return $"Round {Round} : Pick {Position}";
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Round;
        yield return Position;
    }
}
