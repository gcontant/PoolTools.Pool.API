using FluentAssertions;
using PoolTools.Pool.API.Domain.Exceptions;
using PoolTools.Pool.API.Domain.ValueObjects;

namespace Domain.UnitTests.ValueObjects
{
    public class TeamTests
    {
        [Fact]
        public void ShouldCreateTeamWithValidCode()
        {
            string code = "TST";

            var team = Team.From(code);

            team.Code.Should().Be(code);
        }

        [Fact]
        public void ToStringShouldReturnCode()
        {
            string code = "TST";
            var team = Team.From(code);

            team.ToString().Should().Be(team.Code);
        }

        [Fact]
        public void ShouldPerformImplicitConversionToTeamCodeString()
        {
            string code = "TST";
            string team = Team.From(code);

            team.Should().Be(code);
        }

        [Fact]
        public void ShouldPerformExplicitConversionGivenTeamCode()
        {
            var team = (Team)"TST";

            team.Code.Should().Be("TST");
        }


        [Theory]
        [InlineData("TST", "TST", true)]
        [InlineData("TST", "STS", false)]
        [InlineData("TST", "", false)]
        [InlineData("TST", null, false)]
        public void ShouldCompareTeams(string teamCode1, string teamCode2, bool expectedResult)
        {
            var team1 = Team.From(teamCode1);
            var team2 = Team.From(teamCode2);

            team1.Equals(team2).Should().Be(expectedResult);
        }

    }
}