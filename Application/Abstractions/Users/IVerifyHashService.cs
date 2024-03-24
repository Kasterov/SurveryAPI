namespace Application.Abstractions.Users;

public interface IVerifyHashService
{
    bool VerifyHash(string password, byte[] passwordHash, byte[] passwordSalt);
}
