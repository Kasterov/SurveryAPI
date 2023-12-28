using Application.Abstractions.Common;
using Application.Abstractions.Posts;
using Application.DTOs.Posts;
using Application.DTOs.Votes;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

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

    public async Task<Post> Delete(int id)
    {
        var postToDelete = await _context.Posts.FirstOrDefaultAsync(ass => ass.Id == id);

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

    //public async Task<IEnumerable<PostLiteDTO>> GetAll()
    //{
    //    List<Post> postList = await _context.Posts
    //        .Include(post => post.Options)
    //        .ThenInclude(opt => opt.Votes)
    //        .ToListAsync();

    //    List<PostLiteDTO> postLiteDTOList = new List<PostLiteDTO>();

    //    foreach (var post in postList)
    //    {
    //        User author = await _context.Users.SingleAsync(user => user.Id == post.AuthorId);

    //        AuthorDTO authorDTO = new AuthorDTO()
    //        {
    //            Name = author.Name,
    //            Id = author.Id,
    //        };

    //        var votes = post.Options.Select(opt => new VoteLiteDTO()
    //        {
    //            Id = opt.Id,
    //            Option = opt.Title,
    //            Count = opt.Votes.Count
    //        });

    //        PostLiteDTO postLiteDTO = new PostLiteDTO()
    //        {
    //            Id = post.Id,
    //            Title = post.Title,
    //            Author = authorDTO,
    //            Created = post.Created,
    //            LastModified = post.LastModified,
    //            Votes = votes
    //        };

    //        postLiteDTOList.Add(postLiteDTO);
    //    }

    //    return postLiteDTOList;
    //}

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

    public Task<Post> Update(Post assignment)
    {
        throw new NotImplementedException();
    }
}
