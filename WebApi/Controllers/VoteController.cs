using Application.DTOs.Posts;
using Application.DTOs.Votes;
using Application.MediatR.Posts.Commands;
using Application.MediatR.Posts.Queries;
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

    //[HttpGet("assignments")]
    //public async Task<IActionResult> GetAllAssignments([FromQuery] GetAllAssignments query)
    //{
    //    var result = await _mediator.Send(query);

    //    return Ok(result);
    //}

    [HttpGet("vote")]
    public async Task<IActionResult> GetVoteById(int id)
    {
        var result = await _mediator.Send(new GetVoteById(id));

        return Ok(result);
    }

    //[HttpPut("assignment")]
    //public async Task<IActionResult> EditAssignment([FromBody] EditAssignment command)
    //{
    //    var result = await _mediator.Send(command);

    //    return Ok(result);
    //}

    [HttpDelete("vote")]
    public async Task<IActionResult> DeleteVoteById([FromQuery] DeleteVoteById command)
    {
        var result = await _mediator.Send(command);

        return Ok(result);
    }
}