using Application.DTOs.Votes;
using Application.MediatR.Votes.Commands;
using Application.MediatR.Votes.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class VoteController : ControllerBase
{
    private readonly IMediator _mediator;
    public VoteController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("vote")]
    public async Task<IActionResult> CreateVote(CreateVoteDTO command)
    {
        var result = await _mediator.Send(new CreateVote(command));

        return Ok(result);
    }

    [HttpPost("vote-list")]
    public async Task<IActionResult> CreateVoteList(CreateVoteListDTO command)
    {
        var result = await _mediator.Send(new CreateVoteList(command));

        return Ok(result);
    }

    [HttpGet("vote")]
    public async Task<IActionResult> GetVoteById(int id)
    {
        var result = await _mediator.Send(new GetVoteById(id));

        return Ok(result);
    }

    [HttpDelete("vote")]
    public async Task<IActionResult> DeleteVoteById([FromQuery] DeleteVoteById command)
    {
        var result = await _mediator.Send(command);

        return Ok(result);
    }
}