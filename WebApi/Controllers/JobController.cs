using Application.DTOs.Profiles;
using Application.MediatR.Jobs.Queries;
using Application.MediatR.Profiles.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class JobController : ControllerBase
{
    private readonly IMediator _mediator;
    public JobController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("jobs")]
    public async Task<IActionResult> GetJobs()
    {
        var result = await _mediator.Send(new GetJobList());

        return Ok(result);
    }
}
