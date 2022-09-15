using FluentAssertions;
using PoolTools.Pool.API.Domain.Exceptions;
using PoolTools.Pool.API.Domain.ValueObjects;

namespace Domain.UnitTests.ValueObjects;

public class PositionTests
{
    [Fact]
    public void ShouldCreatePositionWithValidCode()
    {
        var code = "RW";

        var position = Position.From(code);

        position.Code.Should().Be(code);
    }

    [Fact]
    public void ToStringShouldReturnCode()
    {
        var position = Position.LeftWing;

        position.ToString().Should().Be(position.Code);
    }

    [Fact]
    public void ShouldPerformImplicitConversionToPositionCodeString()
    {
        string position = Position.Defenseman;

        position.Should().Be("D");
    }

    [Fact]
    public void ShouldPerformExplicitConversionGivenSupportedPositionCode()
    {
        var position = (Position)"G";

        position.Should().Be(Position.Goaltender);
    }

    [Fact]
    public void ShouldThrowUnsupportedPositionExceptionGivenNotSupportedPositionCode()
    {
        FluentActions.Invoking(() => Position.From("R"))
            .Should().Throw<UnsupportedPositionException>();
    }

    [Theory]
    [InlineData("C", true)]
    [InlineData("RW", true)]
    [InlineData("LW", true)]
    [InlineData("D", false)]
    [InlineData("G", false)]
    public void ShouldDetectForwardPositions(string positionCode, bool isForward)
    {
        var position = Position.From(positionCode);

        position.IsForward.Should().Be(isForward);
    }
}
