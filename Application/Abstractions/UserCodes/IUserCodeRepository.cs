using Domain.Entities;
using Domain.Enums;

namespace Application.Abstractions.UserCodes;

public interface IUserCodeRepository
{
    public Task<bool> Add(UserCode userCode);
    public Task<UserCode?> GetByUserIdAndType(string code, UserCodeType type);
    public Task<bool> DeleteByUserIdAndType(UserCode userCode);
}
