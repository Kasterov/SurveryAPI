using Application.DTOs.Common;
using Application.DTOs.PoolOptions;
using Application.DTOs.Users;
using Domain.Entities;

namespace Application.DTOs.Votes;

public class VoteDTO : BaseDTO
{
    public UserDTO User { get; set; }
    public int PoolOptionId { get; set; }
}
