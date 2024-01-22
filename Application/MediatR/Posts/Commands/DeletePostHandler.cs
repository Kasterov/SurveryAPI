using Application.Abstractions.Posts;
using Application.Abstractions.Users;
using Application.DTOs.Posts;
using AutoMapper;
using MediatR;

namespace Application.MediatR.Posts.Commands;

public record DeletePostById(int Id) : IRequest<PostDTO>;

public class DeletePostByIdHandler : IRequestHandler<DeletePostById, PostDTO>
{
    private readonly IPostRepository _repository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IIdentity _identity;

    public DeletePostByIdHandler(
        IPostRepository repository,
        IMapper mapper,
        IUserRepository userRepository,
        IIdentity identity)
    {
        _repository = repository;
        _mapper = mapper;
        _userRepository = userRepository;
        _identity = identity;
    }

    public async Task<PostDTO> Handle(DeletePostById request, CancellationToken cancellationToken)
    {
        var post = await _repository.Delete(request.Id, Convert.ToInt32(_identity.UserId));
        var result = _mapper.Map<PostDTO>(post);

        return result;
    }
}
