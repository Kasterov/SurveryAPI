using Application.Abstractions.Common;
using Application.Abstractions.UserCodes;
using Application.Abstractions.Users;
using Application.DTOs.Users;
using AutoMapper;
using Domain.Entities;
using MailKit.Net.Smtp;
using MailKit.Security;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MimeKit;
using MimeKit.Text;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using static System.Net.Mime.MediaTypeNames;

namespace Application.MediatR.Users.Commands;

public record CreateUser(CreateUserDTO dto) : IRequest<UserDTO>;

public class CreateUserHandler : IRequestHandler<CreateUser, UserDTO>
{
    private readonly IUserRepository _uesrRepository;
    private readonly IUserCodeRepository _uesrCodeRepository;
    private readonly IMapper _mapper;
    private readonly IMailSenderService _mailSenderService;
    private readonly ICodeGenerator _codeGenerator;

    public CreateUserHandler(
        IUserRepository userRepository,
        IUserCodeRepository userCodeRepository,
        IMapper mapper,
        IMailSenderService mailSenderService,
        ICodeGenerator codeGenerator)
    {
        _uesrRepository = userRepository;
        _uesrCodeRepository = userCodeRepository;
        _mapper = mapper;
        _mailSenderService = mailSenderService;
        _codeGenerator = codeGenerator;
    }

    public async Task<UserDTO> Handle(CreateUser request, CancellationToken cancellationToken)
    {
        CreateUserDTO dto = request.dto;

        _codeGenerator.GenerateHash(dto.Password, out byte[] passwordHash, out byte[] passwordSalt);

        var user = new User()
        {
            Name = dto.Name,
            Email = dto.Email,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            DateOfBirth = dto.DateOfBirth,
            IsActive = false
        };

        var entity = await _uesrRepository.Add(user);
        var result = _mapper.Map<UserDTO>(entity);

        var codeToCompleteRegistration = _codeGenerator.GenerateCode();

        var createdCode = await _uesrCodeRepository.Add(new()
        {
            UserId = result.Id,
            Code = codeToCompleteRegistration,
            Type = Domain.Enums.UserCodeType.MailConfirmCode
        });

        _mailSenderService.SendMail("SurveyUa registration", $"Complete registration: <a href=\"http://localhost:5173/#/verify-email/{codeToCompleteRegistration}\">Verify email</a>", request.dto.Email);

        return result;
    }
}
