using FluentValidation;

namespace Application.PoolTeams.Commands;

public sealed class TradeCommandValidator: AbstractValidator<TradeCommand>
{
    public TradeCommandValidator()
    {
        RuleFor(t => t.TradePartnerTeamId)
            .GreaterThan(0).WithMessage("Trade partner team Id must be greater than 0.")
            .MustAsync(BeExistingTeam).WithMessage("Can't trade with inexistant team.");

        RuleFor(t => t)
            .MustAsync(HaveTradedPlayers).WithMessage("Can't trade players not on your team.")
            .MustAsync(HaveTradedPicks).WithMessage("Can't trade picks that don't belong to you.")
            .MustAsync(HaveReceivedPlayers).WithMessage("Can't trade for players not on partner's team.")
            .MustAsync(HaveReceivedPicks).WithMessage("Can't trade for picks not belonging to the trade partner.");
    }

    private Task<bool> HaveReceivedPicks(TradeCommand trade, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
    private Task<bool> HaveReceivedPlayers(TradeCommand trade, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    private Task<bool> HaveTradedPicks(TradeCommand trade, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    private Task<bool> HaveTradedPlayers(TradeCommand trade, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    private Task<bool> BeExistingTeam(int teamId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
