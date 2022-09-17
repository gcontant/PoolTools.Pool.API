using Application.Common.Extensions;
using Application.Common.Interfaces;
using Application.Players.Queries;
using Application.PoolTeams.Queries;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PoolTools.Pool.API.Domain.Entities;
using PoolTools.Pool.API.Domain.ValueObjects;
using System.Globalization;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlayerController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IApplicationContext _context;
    private readonly IConfiguration _configuration;

    public PlayerController(IApplicationContext context, IConfiguration configuration, IMediator mediator)
    {
        _context = context;
        _configuration = configuration;
        _mediator = mediator;
    }

    [HttpGet("undrafted")]
    public async Task<ActionResult<List<PlayerDto>>> GetUndraftedPlayers()
    {
        return await _mediator.Send(new GetUndraftedPlayersQuery());
    }

    //TODO: Replace with event integration to maintain player
    [HttpPost("batch/{year}")]
    public async Task<ActionResult> LoadPlayer(int year)
    {
        _context.Players.RemoveRange(_context.Players);

        //Get latest blob from storage
        var connectionString = _configuration["AzureStorage:ConnectionString"];
        var blobServiceClient = new BlobServiceClient(connectionString);

        var container = blobServiceClient.GetBlobContainerClient("capfriendlydata");

        var lastModified = DateTimeOffset.MinValue;
        BlobItem lastBlob = default!;
        await foreach (var blobItem in container.GetBlobsAsync(prefix: $"CapFriendly_{year}"))
        {
            if (blobItem.Properties.LastModified > lastModified)
            {
                lastBlob = blobItem;
                lastModified = blobItem.Properties.LastModified.Value;
            }

        }

        var blobClient = container.GetBlobClient(lastBlob.Name);

        //Parse CSV
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            PrepareHeaderForMatch = args => args.Header.ToUpper(),
        };

        using var stream = new MemoryStream();

        await blobClient.DownloadToAsync(stream);
        stream.Position = 0;
        
        using var reader = new StreamReader(stream);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        var players = csv.GetRecords<ParsedPlayer>();

        //Insert player
        var mappedPlayers = new List<Player>();
        foreach (var player in players)
        {
            var mappedPlayer = new Player
            {
                FullName = player.FullName[(player.FullName.IndexOf('.') + 2)..],
                Position = Position.From(player.Position.NormalizePosition()),
                Team = Team.From(player.Team),
                AAV = decimal.Parse(player.AAV, NumberStyles.Currency)
            };

            mappedPlayers.Add(mappedPlayer);
        }

        _context.Players.AddRange(mappedPlayers);
        await _context.SaveChangesAsync(CancellationToken.None);

        return Ok();
    }
}

internal class ParsedPlayer
{
    [Name("PLAYER")]
    public string FullName { get; set; } 
    
    [Name("TEAM")]
    public string Team { get; set; }
    [Name("DATE OF BIRTH")]
    public DateTime DateOfBirth { get; set; }
    [Name("POS")]
    public string Position { get; set; }
    [Name("EXPIRY")]
    public string ExpiryStatus { get; set; }
    [Name("EXP. YEAR")]
    public int ExpiryYear { get; set; }
    [Name("CAP HIT")]
    public string CapHit { get; set; } 
    public string AAV { get; set; }
    [Name("SALARY")]
    public string Salary { get; set; }
}
