using Application.DTOs.Common;
using Application.DTOs.PoolOptions;
using Application.DTOs.Users;
using Domain.Entities;

namespace Application.DTOs.Posts;

public class PostDTO : BaseDTO
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public int AuthorId { get; set; }
    public bool IsMultiple { get; set; }
    public IEnumerable<PoolOptionDTO> Options { get; set; }
}
