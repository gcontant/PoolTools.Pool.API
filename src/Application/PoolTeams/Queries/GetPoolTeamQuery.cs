using Application.Common.Interfaces;
using Application.Pools.Queries.GetPools;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.PoolTeams.Queries;

public sealed class GetPoolTeamQuery : IRequest<DetailedPoolTeamDto>
{
    public int PoolId { get; set; }
    public int TeamId { get; set; }
}

public sealed class GetPoolTeamQueryHandler : IRequestHandler<GetPoolTeamQuery, DetailedPoolTeamDto>
{
    private readonly IApplicationContext _context;
    private readonly IMapper _mapper;

    public GetPoolTeamQueryHandler(IApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<DetailedPoolTeamDto> Handle(GetPoolTeamQuery request, CancellationToken cancellationToken)
    {
        var pool = await _context.Pools.AsNoTracking()
            .Include(p => p.Teams)
            .ThenInclude(t => t.DraftPicks)
            .FirstAsync((p) => p.Id == request.PoolId,cancellationToken);

        var team = pool.Teams.Single(t => t.Id == request.TeamId);

        return _mapper.Map<DetailedPoolTeamDto>(team);
    }
}
