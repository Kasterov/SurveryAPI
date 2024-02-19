
using Application.DTOs.PoolOptions;
using Domain.Entities;

namespace Application.DTOs.Posts;

public class CreatePostDTO
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public int AuthorId { get; set; }
    public bool IsMultiple { get; set; }
    public bool IsRevotable { get; set; }
    public IEnumerable<PoolOptionCreateDTO> Options { get; set; }
}
