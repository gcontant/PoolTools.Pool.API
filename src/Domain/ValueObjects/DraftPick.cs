namespace PoolTools.Pool.API.Domain.ValueObjects;

public class DraftPick : ValueObject
{
    public int Round { get; set; }
    public int Position { get; set; }

    public int OverallPosition => Position + (Round-1) * Position;

    static DraftPick()
    { }

    private DraftPick(int round, int position)
    {
        Round = round;
        Position = position;
    }

    public static DraftPick From(int round, int position)
    {
        if (IsInvalidRound(round) || IsInvalidPosition(position))
        {
            throw new UnsupportedDraftPickException(round, position);
        }

        return new DraftPick(round, position);
    }

    private static bool IsInvalidPosition(int position)
    {
        //TODO Reset it back to 12 after initial load
        return position < 0 || position > 13;
    }

    private static bool IsInvalidRound(int round)
    {
        return round < 0 || round > 5;
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
