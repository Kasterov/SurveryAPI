using Application.Abstractions.Posts;
using Application.DTOs.Posts;
using AutoMapper;
using MediatR;

namespace Application.MediatR.Posts.Commands;

public record DeletePostById(int Id) : IRequest<PostDTO>;

public class DeletePostByIdHandler : IRequestHandler<DeletePostById, PostDTO>
{
    private readonly IPostRepository _repository;
    private readonly IMapper _mapper;

    public DeletePostByIdHandler(IPostRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PostDTO> Handle(DeletePostById request, CancellationToken cancellationToken)
    {
        var post = await _repository.Delete(request.Id);
        var result = _mapper.Map<PostDTO>(post);

        return result;
    }
}
