using Application.MediatR.Countries.Queries;
using Application.MediatR.Educations.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CountryController : ControllerBase
{
    private readonly IMediator _mediator;
    public CountryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("countries")]
    public async Task<IActionResult> GetCountries()
    {
        var result = await _mediator.Send(new GetCountryList());

        return Ok(result);
    }
}
