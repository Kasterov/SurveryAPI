using Application.Abstractions.Educations;
using Application.Abstractions.Files;
using Application.DTOs.Educations;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MediatR.Files.Queries;

public record GetFile(int id) : IRequest<FileEntity>;

public class GetEducationListHandler : IRequestHandler<GetFile, FileEntity>
{
    private readonly IFileEntityRepository _repository;

    public GetEducationListHandler(IFileEntityRepository repository)
    {
        _repository = repository;
    }

    public async Task<FileEntity?> Handle(GetFile request, CancellationToken cancellationToken)
    {
        var file = await _repository.GetFile(request.id);

        return file;
    }
}
