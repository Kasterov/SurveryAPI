using Application.DTOs.FileEntities;
using Application.MediatR.Educations.Queries;
using Application.MediatR.Files.Commands;
using Application.MediatR.Files.Queries;
using Application.MediatR.Hobbies.Queires;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class FileController : ControllerBase
{
    private readonly IMediator _mediator;
    public FileController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("file")]
    public async Task<IActionResult> UploadFile(UploadFileDTO file)
    {
        var result = await _mediator.Send(new UploadFile(file));

        return Ok(result);
    }

    [HttpGet("file-content")]
    public async Task<IActionResult> GetFileAsStream(int id)
    {
        Thread.Sleep(2000);

        var file = await _mediator.Send(new GetFile(id));

        return new FileContentResult(file.Bytes, file.ContentType);
    }
}
