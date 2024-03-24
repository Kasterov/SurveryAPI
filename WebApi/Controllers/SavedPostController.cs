using Application.DTOs.General;
using Application.DTOs.Posts;
using Application.DTOs.SavedPosts;
using Application.MediatR.Posts.Commands;
using Application.MediatR.Posts.Queries;
using Application.MediatR.SavedPosts.Commands;
using Application.MediatR.SavedPosts.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class SavedPostController : ControllerBase
{
    private readonly IMediator _mediator;
    public SavedPostController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize]
    [HttpPost("saved-post")]
    public async Task<IActionResult> CreateSavedPost([FromBody] CreateSavedPostDTO dto)
    {
        var result = await _mediator.Send(new CreateSavedPost(dto));

        return Ok(result);
    }

    [Authorize]
    [HttpDelete("saved-post-delete")]
    public async Task<IActionResult> DeleteSavedPost([FromBody] DeleteSavedPostByIdDTO dto)
    {
        var result = await _mediator.Send(new DeleteSavedPost(dto));

        return Ok(result);
    }

    [Authorize]
    [HttpGet("saved-post")]
    public async Task<IActionResult> IsSavedPost([FromQuery] int postId)
    {
        var result = await _mediator.Send(new IsSavedPost(postId));

        return Ok(result);
    }
}
