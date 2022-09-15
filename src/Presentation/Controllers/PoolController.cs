using Application.Pools.Queries.GetPools;
using Application.PoolTeams.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PoolController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<PoolController> _logger;

        public PoolController(IMediator mediator, ILogger<PoolController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<PoolDto>>> GetPools()
        {
            return await _mediator.Send(new GetPoolsQuery());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PoolDto>> GetPools(int id)
        {
            return await _mediator.Send(new GetPoolByIdQuery { poolId = id });
        }

        [HttpGet("{poolId}/{teamId}")]
        public async Task<ActionResult<DetailedPoolTeamDto>> GetPoolTeam(int poolId, int teamId)
        {
            return await _mediator.Send(new GetPoolTeamQuery { PoolId = poolId, TeamId = teamId });
        }
    }
}
