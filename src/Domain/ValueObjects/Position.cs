using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PoolTools.Pool.API.Domain.ValueObjects;
public class Position : ValueObject
{
    public string Code { get; private set; } = "C";
    
    [NotMapped]
    public bool IsForward => ForwardPositions.Contains(Code);


    static Position()
    {
    }

    private Position()
    {
    }

    private Position(string code)
    {
        Code = code;
    }

    public static Position From(string code)
    {
        var position = new Position { Code = code };

        if (!SupportedPositions.Contains(position))
        {
            throw new UnsupportedPositionException(code);
        }

        return position;
    }

    public static Position Center => new("C");
    public static Position RightWing => new("RW");
    public static Position LeftWing => new("LW");
    public static Position Defenseman => new("D");
    public static Position Goaltender => new("G");


    protected static IEnumerable<Position> SupportedPositions
    {
        get
        {
            yield return Center;
            yield return RightWing;
            yield return LeftWing;
            yield return Defenseman;
            yield return Goaltender;
        }
    }

    protected static IEnumerable<string> ForwardPositions
    {
        get
        {
            yield return Center.Code;
            yield return RightWing.Code;
            yield return LeftWing.Code;
        }
    }

    public static implicit operator string(Position position)
    {
        return position.ToString();
    }

    public static explicit operator Position(string code)
    {
        return From(code);
    }

    public override string ToString()
    {
        return Code;
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Code;
    }
}
