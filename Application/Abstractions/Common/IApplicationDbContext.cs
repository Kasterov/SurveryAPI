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
    DbSet<Country> Countries { get; }
    DbSet<Profile> Profiles { get; }
    DbSet<ProfileJob> ProfileJobs { get; }
    DbSet<ProfileHobby> ProfileHobbies { get; }
    DbSet<ProfileEducation> ProfileEducations { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
