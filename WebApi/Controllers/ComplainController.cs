using Application.DTOs.Complains;
using Application.DTOs.General;
using Application.MediatR.Complains.Commands;
using Application.MediatR.Complains.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ComplainController : ControllerBase
{
    private readonly IMediator _mediator;
    public ComplainController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize]
    [HttpPost("complain")]
    public async Task<IActionResult> CreateComplain(CreateComplainDTO dto)
    {
        var result = await _mediator.Send(new CreateComplain(dto));

        return Ok(result);
    }

    [HttpGet("complain")]
    public async Task<IActionResult> GetComplainById(int complainId)
    {
        var result = await _mediator.Send(new GetComplainById(complainId));

        return Ok(result);
    }

    [HttpGet("complain-list")]
    public async Task<IActionResult> GetComplainList([FromQuery] PaginationRequestDTO paginationRequestDTO)
    {
        var result = await _mediator.Send(new GetComplainList(paginationRequestDTO));

        return Ok(result);
    }
}
