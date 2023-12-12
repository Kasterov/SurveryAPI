using Application.Abstractions.Users;
using Application.DTOs.Users;
using AutoMapper;
using MediatR;

namespace Application.MediatR.Users.Queries;

public record GetUserById(int Id) : IRequest<UserDTO>;

public class GetUserByIdHandler : IRequestHandler<GetUserById, UserDTO>
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;

    public GetUserByIdHandler(IUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<UserDTO> Handle(GetUserById request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetById(request.Id);
        var result = _mapper.Map<UserDTO>(user);

        return result;
    }
}
