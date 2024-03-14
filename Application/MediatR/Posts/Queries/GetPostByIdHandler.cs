using Application.Abstractions.Posts;
using Application.Abstractions.Users;
using Application.DTOs.Posts;
using Application.DTOs.Users;
using AutoMapper;
using MediatR;

namespace Application.MediatR.Posts.Queries;

public record GetPostById(int Id) : IRequest<PostEditDTO>;

public class GetPostByIdHandler : IRequestHandler<GetPostById, PostEditDTO>
{
    private readonly IPostRepository _repository;

    public GetPostByIdHandler(IPostRepository repository)
    {
        _repository = repository;
    }

    public async Task<PostEditDTO> Handle(GetPostById request, CancellationToken cancellationToken)
    {
        var postEdit = await _repository.GetPostEditById(request.Id);

        return postEdit;
    }
}