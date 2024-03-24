namespace Application.Abstractions.UserCodes;

public interface ICodeGenerator
{
    public string GenerateCode();

    public void GenerateHash(string str, out byte[] passwordHash, out byte[] passwordSalt);
}
