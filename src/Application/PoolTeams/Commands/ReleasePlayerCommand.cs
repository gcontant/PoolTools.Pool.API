using Application.Common.Interfaces;
using MediatR;

namespace Application.PoolTeams.Commands;

public sealed class ReleasePlayerCommand : IRequest<Unit>
{
    public int TeamId { get; set; }
    public int PlayerId { get; set; }
}

public sealed class ReleasePlayerCommandHandler : IRequestHandler<ReleasePlayerCommand, Unit>
{
    private readonly IApplicationContext _context;

    public ReleasePlayerCommandHandler(IApplicationContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(ReleasePlayerCommand request, CancellationToken cancellationToken)
    {
        var team = await _context.PoolTeams.FindAsync(new object[] { request.TeamId });
        var player = await _context.Players.FindAsync(new object[] { request.PlayerId });

        team!.Release(player!);

        return Unit.Value;
    }
}
