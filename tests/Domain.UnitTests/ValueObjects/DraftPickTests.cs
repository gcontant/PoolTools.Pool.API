using FluentAssertions;
using PoolTools.Pool.API.Domain.Exceptions;
using PoolTools.Pool.API.Domain.ValueObjects;

namespace Domain.UnitTests.ValueObjects
{
    public class DraftPickTests
    {
        [Fact]
        public void ShouldCreateDraftPickWithValidRoundAndPosition()
        {
            int round = 4;
            int position = 10;

            var pick = DraftPick.From(round, position);

            pick.Round.Should().Be(round);
            pick.Position.Should().Be(position);
        }

        [Fact]
        public void ShouldThrowWhenCreatingDraftPickWithNegativeRound()
        {
            int round = -1;
            int position = 10;

            var act = () => DraftPick.From(round, position);

            act.Should().Throw<UnsupportedDraftPickException>().WithMessage("Draft pick for round -1 and position 10 is unsupported.");
        }

        [Fact]
        public void ShouldThrowWhenCreatingDraftPickWithRoundTooHigh()
        {
            int round = 11;
            int position = 10;

            var act = () => DraftPick.From(round, position);

            act.Should().Throw<UnsupportedDraftPickException>().WithMessage("Draft pick for round 11 and position 10 is unsupported.");
        }

        [Fact]
        public void ShouldThrowWhenCreatingDraftPickWithNegativePosition()
        {
            int round = 1;
            int position = -10;

            var act = () => DraftPick.From(round, position);

            act.Should().Throw<UnsupportedDraftPickException>().WithMessage("Draft pick for round 1 and position -10 is unsupported.");
        }

        [Fact]
        public void ShouldThrowWhenCreatingDraftPickWithPositionTooHigh()
        {
            int round = 1;
            int position = 100;

            var act = () => DraftPick.From(round, position);

            act.Should().Throw<UnsupportedDraftPickException>().WithMessage("Draft pick for round 1 and position 100 is unsupported.");
        }

        [Fact]
        public void ToStringShouldReturnRoundAndPosition()
        {
            int round = 4;
            int position = 10;

            var pick = DraftPick.From(round, position);

            pick.ToString().Should().Be($"Round {round} : Pick {position}");
        }

        [Theory]
        [InlineData(1, 1, 1, 1, true)]
        [InlineData(1, 1, 1, 2, false)]
        [InlineData(1, 1, 2, 1, false)]
        [InlineData(1, 1, 2, 2, false)]
        public void ShouldCompareDraftPicks(int pick1Round, int pick1Position, int pick2Round, int pick2Position, bool expectedResult)
        {
            var pick1 = DraftPick.From(pick1Round, pick1Position);
            var pick2 = DraftPick.From(pick2Round, pick2Position);

            pick1.Equals(pick2).Should().Be(expectedResult);
        }
    }
}