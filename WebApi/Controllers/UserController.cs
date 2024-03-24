using Application.DTOs.Profiles;
using Application.DTOs.Users;
using Application.MediatR.Profiles.Commands;
using Application.MediatR.Profiles.Queries;
using Application.MediatR.Users.Commands;
using Application.MediatR.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("verify-email")]
    public async Task<IActionResult> VerifyEmail([FromBody] string code)
    {
        var result = await _mediator.Send(new VerifyUserEmail(code));

        return Ok(result);
    }

    [HttpPost("user")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDTO command)
    {
        var result = await _mediator.Send(new CreateUser(command));

        return Ok(result);
    }

    [HttpPost("sign-in")]
    public async Task<IActionResult> SignInUser([FromBody] SignInDTO query)
    {
        var result = await _mediator.Send(new SignIn(query));

        return Ok(result);
    }

    [HttpGet("profile-view")]
    public async Task<IActionResult> GetProfileViewData(int id)
    {
        var result = await _mediator.Send(new GetProfileViewData(id));

        return Ok(result);
    }

    [Authorize]
    [HttpPost("profile")]
    public async Task<IActionResult> CreateProfile(UserProfileUpdateDTO command)
    {
        var result = await _mediator.Send(new UpdateUserProfile(command));

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