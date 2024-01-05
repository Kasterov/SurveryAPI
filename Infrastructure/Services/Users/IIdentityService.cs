using Application.Abstractions.Users;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Infrastructure.Services.Users
{
    public class UserIdentity : IIdentity
    {
        private readonly IHttpContextAccessor contextAccessor;
        public UserIdentity(IHttpContextAccessor contextAccessor)
        {
            this.contextAccessor = contextAccessor;
        }
        public string UserId => contextAccessor.HttpContext!.User.FindFirstValue(JwtRegisteredClaimNames.Sid);
    }
}