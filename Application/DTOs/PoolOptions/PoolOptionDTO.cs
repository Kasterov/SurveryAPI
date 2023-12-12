using Application.DTOs.Votes;
using Domain.Entities;

namespace Application.DTOs.PoolOptions;

public class PoolOptionDTO
{
    public string Title { get; set; }
    public IEnumerable<VoteDTO> Votes { get; set; }
}
