using Application.DTOs.Common;
using Application.DTOs.PoolOptions;
using Application.DTOs.Votes;
using Domain.Entities;

namespace Application.DTOs.Posts;

public class PostTableDTO : BaseDTO
{
    public string Title { get; set; }
    public int Status { get; set; }
    public int Votes { get; set; }
    public int People { get; set; }
}

public class PostTableFullDTO : BaseDTO
{
    public string Title { get; set; }
    public int Status { get; set; }
    public IEnumerable<PoolOptionDTO> PoolOptions { get; set; }
}