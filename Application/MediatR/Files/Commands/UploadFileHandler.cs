using Application.Abstractions.Files;
using Application.Abstractions.Posts;
using Application.DTOs.FileEntities;
using Application.DTOs.Posts;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MediatR.Files.Commands;

public record UploadFile(UploadFileDTO file) : IRequest<bool>;

public class CreatePostHandler : IRequestHandler<UploadFile, bool>
{
    private readonly IFileEntityRepository _repository;

    public CreatePostHandler(IFileEntityRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(UploadFile request, CancellationToken cancellationToken)
    {
        FileEntity file = new()
        {
            ContentType = request.file.ContentType,
            Bytes = System.Convert.FromBase64String(request.file.Base64),
            Name = request.file.Name,
            Expression = request.file.Expression,
        };

        var res = await _repository.UploadFile(file);

        return res;
    }
}