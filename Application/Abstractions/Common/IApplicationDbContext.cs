using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Application.Abstractions.Common;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    DbSet<Post> Posts { get; }
    DbSet<PoolOption> PoolOptions { get; }
    DbSet<Vote> Votes { get; }
    DbSet<Education> Educations { get; }
    DbSet<Job> Jobs { get; }
    DbSet<Hobby> Hobbies { get; }
    DbSet<Media> Files { get; }
    DbSet<Country> Countries { get; }
    DbSet<UserJob> ProfileJobs { get; }
    DbSet<UserHobby> ProfileHobbies { get; }
    DbSet<UserEducation> ProfileEducations { get; }
    DbSet<Complain> Complains { get; }
    DbSet<SavedPost> SavedPosts { get; }
    DbSet<UserCode> UserCodes { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
