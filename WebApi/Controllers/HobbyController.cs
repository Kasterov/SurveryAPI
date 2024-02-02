using Application.MediatR.Educations.Queries;
using Application.MediatR.Hobbies.Queires;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class HobbyController : ControllerBase
{
    private readonly IMediator _mediator;
    public HobbyController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("hobbies")]
    public async Task<IActionResult> GetHobbies()
    {
        var result = await _mediator.Send(new GetHobbyList());

        return Ok(result);
    }
}
