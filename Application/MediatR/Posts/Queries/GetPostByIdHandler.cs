using Application.Abstractions.Posts;
using Application.Abstractions.Users;
using Application.DTOs.Posts;
using Application.DTOs.Users;
using AutoMapper;
using MediatR;

namespace Application.MediatR.Posts.Queries;

public record GetPostById(int Id) : IRequest<PostDTO>;

public class GetPostByIdHandler : IRequestHandler<GetPostById, PostDTO>
{
    private readonly IPostRepository _repository;
    private readonly IMapper _mapper;

    public GetPostByIdHandler(IPostRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PostDTO> Handle(GetPostById request, CancellationToken cancellationToken)
    {
        var post = await _repository.GetById(request.Id);
        var result = _mapper.Map<PostDTO>(post);

        return result;
    }
}