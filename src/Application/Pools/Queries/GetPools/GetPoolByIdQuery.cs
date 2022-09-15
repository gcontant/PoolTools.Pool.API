using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Pools.Queries.GetPools;

public class GetPoolByIdQuery : IRequest<PoolDto>
{
    public int poolId { get; set; }
}

public class GetPoolByIdQueryHandler : IRequestHandler<GetPoolByIdQuery, PoolDto>
{
    private readonly IApplicationContext _context;
    private readonly IMapper _mapper;

    public GetPoolByIdQueryHandler(IApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


    public async Task<PoolDto> Handle(GetPoolByIdQuery request, CancellationToken cancellationToken)
    {
        var pool = await _context.Pools
                    .AsNoTracking()
                    .Include(p => p.Teams)
                    .SingleAsync(p => p.Id == request.poolId, cancellationToken: cancellationToken);

        var poolDto = _mapper.Map<PoolDto>(pool);

        return poolDto;
    }
}
