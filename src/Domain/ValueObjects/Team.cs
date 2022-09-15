using System.ComponentModel.DataAnnotations;

namespace PoolTools.Pool.API.Domain.ValueObjects;

public class Team : ValueObject
{
    public string Code { get; set; } = default!;

    static Team()
    { }

    private Team(string code)
    {
        Code = code;
    }

    public static Team From(string code)
    {
        return new Team(code);
    }

    public static implicit operator string(Team position)
    {
        return position.ToString();
    }

    public static explicit operator Team(string code)
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
