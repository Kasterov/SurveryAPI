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
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
