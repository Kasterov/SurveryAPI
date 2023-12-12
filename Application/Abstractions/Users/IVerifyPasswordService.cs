namespace Application.Abstractions.Users;

public interface IVerifyPasswordService
{
    bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
}
