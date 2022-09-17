using Application.Common.Interfaces;
using Application.Pools.Queries.GetPools;
using AutoMapper;
using AutoMapper.QueryableExtensions;
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
        var team = await _context.PoolTeams.AsNoTracking()
            .Include(t => t.DraftPicks)
            .Include(t => t.Roster)
            .FirstAsync(t => t.Id == request.TeamId, cancellationToken);

        return _mapper.Map<DetailedPoolTeamDto>(team);
    }
}
