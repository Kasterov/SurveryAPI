using Application.Abstractions.Complains;
using Application.Abstractions.Files;
using Application.Abstractions.Users;
using Application.DTOs.Complains;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.MediatR.Complains.Commands;

public record CreateComplain(CreateComplainDTO dto) : IRequest<bool>;

public class CreateComplainHandler : IRequestHandler<CreateComplain, bool>
{
    private readonly IIdentity _identity;
    private readonly IComplainRepository _repository;
    private readonly IMapper _mapper;

    public CreateComplainHandler(
        IIdentity identity,
        IComplainRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
        _identity = identity;
    }

    public async Task<bool> Handle(CreateComplain request, CancellationToken cancellationToken)
    {
        var createComplain = _mapper.Map<Complain>(request.dto);

        createComplain.AccuserId = Convert.ToInt32(_identity.UserId);

        var res = await _repository.CreateComplain(createComplain);

        return res;
    }
}
