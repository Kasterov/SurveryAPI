using Application.Abstractions.Common;
using Application.Abstractions.Posts;
using Application.DTOs.PoolOptions;
using Application.DTOs.Posts;
using Application.DTOs.Votes;
using Azure.Core;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class PostRepository : IPostRepository
{
    private readonly IApplicationDbContext _context;
    public PostRepository(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Post> Add(Post post)
    {
        var result = await _context.Posts.AddAsync(post);
        await _context.SaveChangesAsync();

        return result.Entity;
    }

    public async Task<Post> Delete(int postId, int userId)
    {
        var postToDelete = await _context.Posts.FirstOrDefaultAsync(ass => ass.Id == postId && ass.AuthorId == userId);

        if (postToDelete is null)
        {
            throw new NullReferenceException();
        }

        var result = _context.Posts.Remove(postToDelete);

        if (result is null)
        {
            throw new Exception();
        }

        await _context.SaveChangesAsync();

        return result.Entity;
    }

    public async Task<IEnumerable<PostLiteDTO>> GetAll()
    {

        List<PostLiteDTO> postLiteDTOList = await _context.Posts.Select(post => new PostLiteDTO()
            {
                Id = post.Id,
                Title = post.Title,
                Created = post.Created,
                LastModified = post.LastModified,
                Author = new AuthorDTO()
                {
                    Name = post.Author.Name,
                    Id = post.Author.Id,
                },
                Votes = post.Options.Select(o => new VoteLiteDTO()
                {
                    Id = o.Id,
                    Option = o.Title,
                    Count = o.Votes.Count,
                })
        }).AsNoTracking()
        .ToListAsync();

        return postLiteDTOList;
    }

    public async Task<Post> GetById(int id)
    {
        var post = await _context.Posts
           .Include(post => post.Author)
           .Include(post => post.Options).ThenInclude(opt => opt.Votes)
           .AsNoTracking()
           .FirstOrDefaultAsync(ass => ass.Id == id);

        return post;
    }

    public async Task<IEnumerable<PostTableFullDTO>> GetTablePostList(int userId)
    {
        var posts = await _context.Posts
            .Where(post => post.AuthorId == userId)
            .Select(post => new PostTableFullDTO()
        {
            Id = post.Id,
            Title = post.Title,
            Status = 1,
            Created = post.Created,
            LastModified = post.LastModified,
            PoolOptions = post.Options.Select(opt => new PoolOptionDTO()
            {
                Title = opt.Title,
                Votes = opt.Votes.Select(vote => new VoteDTO() 
                {
                    Id = vote.Id,
                    User = new Application.DTOs.Users.UserDTO()
                    {
                        Name = vote.User.Name,
                        Id = vote.User.Id
                    },
                    PoolOptionId = vote.PoolOptionId
                })
            }),
        }).AsNoTracking()
        .ToListAsync();

        return posts;
    }

    public Task<Post> Update(Post assignment)
    {
        throw new NotImplementedException();
    }
}
