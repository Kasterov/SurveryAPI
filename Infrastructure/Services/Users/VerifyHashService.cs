using Application.Abstractions.Users;
using System.Security.Cryptography;

namespace Infrastructure.Services.Users;

public class VerifyHashService : IVerifyHashService
{
    public bool VerifyHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using (var hmacsha = new HMACSHA512(passwordSalt))
        {
            var computeHash = hmacsha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            return computeHash.SequenceEqual(passwordHash);
        }
    }
}
