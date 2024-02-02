using Application.MediatR.Educations.Queries;
using Application.MediatR.Jobs.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class EducationController : ControllerBase
{
    private readonly IMediator _mediator;
    public EducationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("educations")]
    public async Task<IActionResult> GetEducations()
    {
        var result = await _mediator.Send(new GetEducationList());

        return Ok(result);
    }
}
