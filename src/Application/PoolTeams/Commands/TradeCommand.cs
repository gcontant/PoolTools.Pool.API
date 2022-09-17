using Application.Common.Interfaces;
using Application.PoolTeams.Queries;
using AutoMapper;
using MediatR;
using PoolTools.Pool.API.Domain.Entities;
using PoolTools.Pool.API.Domain.ValueObjects;

namespace Application.PoolTeams.Commands;

public sealed class TradeCommand : IRequest<Unit>
{
    public int TeamId { get; set; }
    public int TradePartnerTeamId { get; set; }
    public IList<TradedPlayerDto>? TradedPlayers { get; set; }
    public IList<TradedPlayerDto>? ReceivedPlayers { get; set; }
    public IList<DraftPickDto>? TradedPicks { get; set; }
    public IList<DraftPickDto>? ReceivedPicks { get; set; }
}

public sealed class TradeCommandHandler : IRequestHandler<TradeCommand, Unit>
{
    private readonly IApplicationContext _context;
    private readonly IMapper _mapper;

    public TradeCommandHandler(IApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(TradeCommand request, CancellationToken cancellationToken)
    {
        var team = await _context.PoolTeams.FindAsync(new object[] { request.TeamId });
        var tradePartnerTeam = await _context.PoolTeams.FindAsync(new object[] { request.TradePartnerTeamId });

        var tradedPlayers = request.TradedPlayers != null ? _context.Players.Where(p => request.TradedPlayers.Select(tp => tp.Id).Contains(p.Id)) : Enumerable.Empty<Player>();

        var receivedPlayers = request.ReceivedPlayers != null ? _context.Players.Where(p => request.ReceivedPlayers.Select(tp => tp.Id).Contains(p.Id)) : Enumerable.Empty<Player>();

        var tradedPicks = request.TradedPicks != null ? team!.DraftPicks.Where(dp => request.TradedPicks.Contains(_mapper.Map<DraftPickDto>(dp))) : Enumerable.Empty<DraftPick>();

        var receivedPicks = request.ReceivedPicks != null ? tradePartnerTeam!.DraftPicks.Where(dp => request.ReceivedPicks.Contains(_mapper.Map<DraftPickDto>(dp))) : Enumerable.Empty<DraftPick>();

        team!.Trade(tradePartnerTeam!, tradedPlayers, receivedPlayers, tradedPicks, receivedPicks);

        return Unit.Value;
    }
}
