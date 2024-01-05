using Application.DTOs.Users;
using Application.MediatR.Users.Commands;
using Application.MediatR.Users.Queries;
using MediatR;
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

    [HttpPost("user")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDTO command)
    {
        var result = await _mediator.Send(new CreateUser(command));

        return Ok(result);
    }

    [HttpPost("signin")]
    public async Task<IActionResult> SignInUser([FromBody] SignInDTO query)
    {
        var result = await _mediator.Send(new SignIn(query));

        return Ok(result);
    }

    //[HttpGet("assignments")]
    //public async Task<IActionResult> GetAllAssignments([FromQuery] GetAllAssignments query)
    //{
    //    var result = await _mediator.Send(query);

    //    return Ok(result);
    //}

    [HttpGet("user")]
    public async Task<IActionResult> GetUserById([FromQuery] GetUserById query)
    {
        var result = await _mediator.Send(query);

        return Ok(result);
    }

    //[HttpPut("assignment")]
    //public async Task<IActionResult> EditAssignment([FromBody] EditAssignment command)
    //{
    //    var result = await _mediator.Send(command);

    //    return Ok(result);
    //}

    //[HttpDelete("assignment")]
    //public async Task<IActionResult> DeleteAssignment([FromQuery] DeleteAssignment command)
    //{
    //    var result = await _mediator.Send(command);

    //    return Ok(result);
    //}
}