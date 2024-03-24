using Application.Abstractions.Users;
using Application.DTOs.Users;
using AutoMapper;
using MediatR;

namespace Application.MediatR.Users.Queries;

public record SignIn(SignInDTO SignInDTO) : IRequest<string>;
public class SignInHandler : IRequestHandler<SignIn, string>
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;
    private readonly IVerifyHashService _verifyPasswordService;
    private readonly IJWTGeneratorService _jwtGeneratorService;

    public SignInHandler(IUserRepository repository,
        IMapper mapper,
        IVerifyHashService verifyPasswordService,
        IJWTGeneratorService jwtGeneratorService)
    {
        _repository = repository;
        _mapper = mapper;
        _verifyPasswordService = verifyPasswordService;
        _jwtGeneratorService = jwtGeneratorService;
    }
    public async Task<string> Handle(SignIn request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetByName(request.SignInDTO.Name);

        if (user is null)
        {
            throw new InvalidDataException("No user with such name!");
        }

        if (!user.IsActive)
        {
            throw new InvalidOperationException("User exist but email is not verified!");
        }

        if (!_verifyPasswordService.VerifyHash(request.SignInDTO.Password, user.PasswordHash, user.PasswordSalt))
        {
            throw new InvalidDataException("Wrong password!");
        }

        return _jwtGeneratorService.GenerateJwtToken(user); ;
    }
}
