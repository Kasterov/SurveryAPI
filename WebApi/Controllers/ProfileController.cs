using Application.DTOs.Posts;
using Application.DTOs.Profiles;
using Application.MediatR.Posts.Commands;
using Application.MediatR.Profiles.Commands;
using Application.MediatR.Profiles.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ProfileController : ControllerBase
{
    private readonly IMediator _mediator;
    public ProfileController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize]
    [HttpPost("profile")]
    public async Task<IActionResult> CreateProfile(ProfileUpdateDTO command)
    {
        var result = await _mediator.Send(new CreateProfile(command));

        return Ok(result);
    }

    [Authorize]
    [HttpGet("profile")]
    public async Task<IActionResult> GetProfileByUserId()
    {
        var result = await _mediator.Send(new GetProfileByUserId());

        return Ok(result);
    }
}
