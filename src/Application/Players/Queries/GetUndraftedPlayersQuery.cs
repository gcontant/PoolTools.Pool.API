using Application.Common.Interfaces;
using Application.PoolTeams.Queries;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Players.Queries;

public sealed class GetUndraftedPlayersQuery : IRequest<List<PlayerDto>>
{
}

public sealed class GetUndraftedPlayersQueryHandler : IRequestHandler<GetUndraftedPlayersQuery, List<PlayerDto>>
{
    private readonly IApplicationContext _context;
    private readonly IMapper _mapper;

    public GetUndraftedPlayersQueryHandler(IApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<PlayerDto>> Handle(GetUndraftedPlayersQuery request, CancellationToken cancellationToken)
    {
        var draftedPlayers = _context.PoolTeams.AsNoTracking().SelectMany(p => p.Roster);

        return await _context.Players.AsNoTracking()
            .Where(p => !draftedPlayers.Contains(p))
            .OrderByDescending(p => p.AAV)
            .ProjectTo<PlayerDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
