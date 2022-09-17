using Application.PoolTeams.Commands;
using Application.PoolTeams.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/pool/{poolId}/team/{teamId}")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<TeamController> _logger;

        public TeamController(IMediator mediator, ILogger<TeamController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }


        [HttpGet()]
        public async Task<ActionResult<DetailedPoolTeamDto>> GetPoolTeam(int poolId, int teamId)
        {
            return await _mediator.Send(new GetPoolTeamQuery { PoolId = poolId, TeamId = teamId });
        }

        [HttpPost("/trade")]
        public async Task<ActionResult> Trade(int poolId, int teamId,[FromBody] TradeDetailDto tradeDetail)
        {
            var tradeCommand = new TradeCommand
            {
                ReceivedPicks = tradeDetail.ReceivedPicks,
                ReceivedPlayers = tradeDetail.ReceivedPlayers,
                TeamId = teamId,
                TradedPicks = tradeDetail.TradedPicks,
                TradedPlayers = tradeDetail.TradedPlayers,
                TradePartnerTeamId = tradeDetail.TradePartnerTeamId
            };

            await _mediator.Send(tradeCommand);

            return Ok();
        }

        [HttpPost("/draft")]
        public async Task<ActionResult> Draft(int poolId, int teamId, [FromBody] int playerId)
        {
            var draftCommand = new DraftPlayerCommand { TeamId = teamId, PlayerId = playerId };

            await _mediator.Send(draftCommand);

            return Ok();
        }

        [HttpPost("/release")]
        public async Task<ActionResult> Release(int poolId, int teamId, [FromBody] int playerId)
        {
            var releaseCommand = new ReleasePlayerCommand { TeamId = teamId, PlayerId = playerId };
            await _mediator.Send(releaseCommand);

            return Ok();
        }
    }
}
