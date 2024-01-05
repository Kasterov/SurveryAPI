using Domain.Entities;

namespace Application.Abstractions.Users;

public interface IJWTGeneratorService
{
    string GenerateJwtToken(User user);
}
