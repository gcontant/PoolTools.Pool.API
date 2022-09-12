using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Pools.Queries.GetPools;

public class GetPoolsQuery : IRequest<List<PoolDto>>
{
}

public class GetPoolsQueryHandler : IRequestHandler<GetPoolsQuery, List<PoolDto>>
{
    private readonly IApplicationContext _context;
    private readonly IMapper _mapper;

    public GetPoolsQueryHandler(IApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<PoolDto>> Handle(GetPoolsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Pools
            .AsNoTracking()
            .ProjectTo<PoolDto>(_mapper.ConfigurationProvider)
            .OrderBy(p => p.Name)
            .ToListAsync();
    }
}
