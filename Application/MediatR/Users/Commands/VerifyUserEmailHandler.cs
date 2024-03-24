using Application.Abstractions.Common;
using Application.Abstractions.UserCodes;
using Application.Abstractions.Users;
using AutoMapper;
using Domain.Enums;
using MediatR;

namespace Application.MediatR.Users.Commands;

public record VerifyUserEmail(string code) : IRequest<bool>;

public class VerifyUserEmailHandler : IRequestHandler<VerifyUserEmail, bool>
{
    private readonly IUserRepository _uesrRepository;
    private readonly IUserCodeRepository _uesrCodeRepository;

    public VerifyUserEmailHandler(
        IUserRepository userRepository,
        IUserCodeRepository userCodeRepository)
    {
        _uesrRepository = userRepository;
        _uesrCodeRepository = userCodeRepository;
    }

    public async Task<bool> Handle(VerifyUserEmail request, CancellationToken cancellationToken)
    {
        var userCode = await _uesrCodeRepository.GetByUserIdAndType(request.code, UserCodeType.MailConfirmCode);

        if (userCode is null)
        {
            return false;
        }

        var deletedCode = await _uesrCodeRepository.DeleteByUserIdAndType(userCode);

        bool isUserActivated = false;

        if (deletedCode)
        {
            isUserActivated = await _uesrRepository.PatchActivateUser(userCode.UserId);
        }

        if (!isUserActivated)
        {
            return false;
        }

        return true;
    }
}
