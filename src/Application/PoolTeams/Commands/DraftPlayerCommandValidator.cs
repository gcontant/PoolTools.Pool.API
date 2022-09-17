using Application.Common.Interfaces;
using FluentValidation;

namespace Application.PoolTeams.Commands;

public sealed class DraftPlayerCommandValidator : AbstractValidator<DraftPlayerCommand>
{
    private readonly IApplicationContext _context;

    public DraftPlayerCommandValidator(IApplicationContext context)
    {
        _context = context;

        RuleFor(c => c.TeamId)
            .MustAsync(BeExistingTeam).WithMessage("Can't draft a player to an inexistant team.");

        RuleFor(c => c.PlayerId)
            .MustAsync(BeExistingPlayer).WithMessage("Can't draft a inexistant player.");
    }

    private async Task<bool> BeExistingPlayer(int playerId, CancellationToken cancellationToken)
    {
        var team = await _context.Players.FindAsync(new object[] { playerId }, cancellationToken);

        return team != null;
    }

    private async Task<bool> BeExistingTeam(int teamId, CancellationToken cancellationToken)
    {
        var team = await _context.PoolTeams.FindAsync(new object[] { teamId }, cancellationToken);

        return team != null;
    }
}
