using Application.Common.Interfaces;
using MediatR;

namespace Application.PoolTeams.Commands;

public sealed class DraftPlayerCommand : IRequest<Unit>
{
    public int TeamId { get; set; }
    public int PlayerId { get; set; }
}

public sealed class DraftPlayerCommandHandler : IRequestHandler<DraftPlayerCommand, Unit>
{
    private readonly IApplicationContext _context;

    public DraftPlayerCommandHandler(IApplicationContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DraftPlayerCommand request, CancellationToken cancellationToken)
    {
        var team = await _context.PoolTeams.FindAsync(new object[] { request.TeamId }, cancellationToken);
        var player = await _context.Players.FindAsync(new object[] { request.PlayerId }, cancellationToken);

        team!.Roster.Add(player!);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
