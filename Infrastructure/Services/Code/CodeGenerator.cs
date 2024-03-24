using Application.Abstractions.UserCodes;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;

namespace Infrastructure.Services.Code;

public class CodeGenerator : ICodeGenerator
{
    public void GenerateHash(string str, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmacsha = new HMACSHA512())
        {
            passwordSalt = hmacsha.Key;
            passwordHash = hmacsha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(str));
        }
    }
    public string GenerateCode()
    {
        return Guid.NewGuid().ToString().Split("-")[0];
    }
}
