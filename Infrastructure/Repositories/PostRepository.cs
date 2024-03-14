using Application.Abstractions.Common;
using Application.Abstractions.Posts;
using Application.DTOs.PoolOptions;
using Application.DTOs.Posts;
using Application.DTOs.Votes;
using Azure.Core;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Diagnostics.Metrics;
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
                AvatarId = post.Author.Profile.FileEntityId,
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

    public async Task<PostEditDTO> GetPostEditById(int id)
    {
        var postEdit = (await _context.Posts
            .Where(post => post.Id == id)
            .Select(p => new PostEditDTO()
            {
                Title = p.Title,
                Description = p.Description,
                IsMultiple = p.IsMultiple,
                IsRevotable = p.IsRevotable,
                IsPrivate = p.IsPrivate,
                LastModified = p.LastModified,
                Created = p.Created,
                Id = p.Id,
                Options = p.Options.Select(o => new PoolOptionEditDTO()
                {
                    Id = o.Id,
                    Title = o.Title
                })
            })
            .AsNoTracking()
            .ToListAsync())
            .FirstOrDefault();

        return postEdit;
    }

    public async Task<PostFullDTO?> GetPostFullDTO(int id)
    {
        var postFullDTOList = (await _context.Posts
            .Where(post => post.Id == id)
            .Select(post => new PostFullDTO()
            {
                Id = post.Id,
                Title = post.Title,
                Description = post.Description,
                Created = post.Created,
                LastModified = post.LastModified,
                IsMultiple = post.IsMultiple,
                IsPrivate = post.IsPrivate,
                IsRevotable = post.IsRevotable,
                Author = new AuthorDTO()
                {
                    Name = post.Author.Name,
                    AvatarId = post.Author.Profile.FileEntityId,
                    Id = post.Author.Id,
                },
                Votes = post.Options.Select(o => new VoteLiteDTO()
                {
                    Id = o.Id,
                    Option = o.Title,
                    Count = o.Votes.Count,
                })
        }).AsNoTracking()
        .ToListAsync())
        .FirstOrDefault();

        return postFullDTOList;
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

    public async Task<bool?> PossibleToRevote(int postId)
    {
        var possibleToRevote = await _context.Posts
            .Select(p => new { p.Id, p.IsRevotable })
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == postId);

        return possibleToRevote?.IsRevotable;
    }

    public async Task<Post> Update(Post postUpdate)
    {
        var postToUpdate = await _context.Posts
            .Include(p => p.Options)
            .FirstOrDefaultAsync(x => x.AuthorId == postUpdate.AuthorId && x.Id == postUpdate.Id);

        if (postToUpdate is null)
        {
            return null;
        }

        postToUpdate.Id = postUpdate.Id;
        postToUpdate.Title = postUpdate.Title;
        postToUpdate.Description = postUpdate.Description;
        postToUpdate.IsMultiple = postUpdate.IsMultiple;
        postToUpdate.IsPrivate = postUpdate.IsPrivate;
        postToUpdate.IsRevotable = postUpdate.IsRevotable;
        postToUpdate.Options = postUpdate.Options;

        var postUpdated = _context.Posts.Update(postToUpdate).Entity;

        var res = await _context.SaveChangesAsync();

        return postUpdated;
    }
}
