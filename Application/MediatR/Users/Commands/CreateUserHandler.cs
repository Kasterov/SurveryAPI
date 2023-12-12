using Application.Abstractions.Assignments;
using Application.Abstractions.Users;
using Application.DTOs.Users;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System.Security.Cryptography;
using System.Xml.Linq;

namespace Application.MediatR.Users.Commands;

public record CreateUser(CreateUserDTO dto) : IRequest<UserDTO>;

public class CreateUserHandler : IRequestHandler<CreateUser, UserDTO>
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;

    public CreateUserHandler(
        IUserRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<UserDTO> Handle(CreateUser request, CancellationToken cancellationToken)
    {
        CreateUserDTO dto = request.dto;

        GeneratePasswordHash(dto.Password, out byte[] passwordHash, out byte[] passwordSalt);

        var user = new User()
        {
            Name = dto.Name,
            Email = dto.Email,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            Gender = dto.Gender,
            DateOfBirth = dto.DateOfBirth
        };

        var entity = await _repository.Add(user);
        var result = _mapper.Map<UserDTO>(entity);

        return result;
    }

    private void GeneratePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmacsha = new HMACSHA512())
        {
            passwordSalt = hmacsha.Key;
            passwordHash = hmacsha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }
}
