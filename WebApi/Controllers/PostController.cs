using Application.DTOs.Posts;
using Application.DTOs.Users;
using Application.MediatR.Posts.Commands;
using Application.MediatR.Posts.Queries;
using Application.MediatR.Users.Commands;
using Application.MediatR.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PostController : ControllerBase
{
    private readonly IMediator _mediator;
    public PostController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize]
    [HttpPost("post")]
    public async Task<IActionResult> CreatePost(CreatePostDTO command)
    {
        var result = await _mediator.Send(new CreatePost(command));

        return Ok(result);
    }

    //[HttpGet("assignments")]
    //public async Task<IActionResult> GetAllAssignments([FromQuery] GetAllAssignments query)
    //{
    //    var result = await _mediator.Send(query);

    //    return Ok(result);
    //}

    [HttpGet("post")]
    public async Task<IActionResult> GetPostById(int id)
    {
        var result = await _mediator.Send(new GetPostById(id));

        return Ok(result);
    }

    [HttpGet("postlite")]
    public async Task<IActionResult> GetPostLiteById(int id)
    {
        var result = await _mediator.Send(new GetPostLiteById(id));

        return Ok(result);
    }

    [HttpGet("postlitelist")]
    public async Task<IActionResult> GetPostLiteList()
    {
        var result = await _mediator.Send(new GetPostLiteList());

        return Ok(result);
    }

    [HttpGet("pooloptionlist")]
    public async Task<IActionResult> GetPoolOptionList(int id)
    {
        var result = await _mediator.Send(new PoolOptionsForVoteByPostId(id));

        return Ok(result);
    }

    [Authorize]
    [HttpGet("postmenu")]
    public async Task<IActionResult> GetPostListTable()
    {
        var result = await _mediator.Send(new GetTableOfPostList());

        return Ok(result);
    }

    //[HttpPut("assignment")]
    //public async Task<IActionResult> EditAssignment([FromBody] EditAssignment command)
    //{
    //    var result = await _mediator.Send(command);

    //    return Ok(result);
    //}

    [Authorize]
    [HttpDelete("post")]
    public async Task<IActionResult> DeletePost(int id)
    {
        var result = await _mediator.Send(new DeletePostById(id));

        return Ok(result);
    }
}