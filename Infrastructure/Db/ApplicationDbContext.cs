using Application.Abstractions.Common;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net.Sockets;

namespace Infrastructure.Db;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext() { }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public virtual DbSet<User> Users => Set<User>();

    public virtual DbSet<Post> Posts => Set<Post>();

    public virtual DbSet<PoolOption> PoolOptions => Set<PoolOption>();

    public virtual DbSet<Vote> Votes => Set<Vote>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<PoolOption>()
            .HasKey(x => x.Id);

        modelBuilder.Entity<Post>()
            .Property(p => p.AuthorId)
            .IsRequired();

        modelBuilder.Entity<Post>()
            .HasKey(x => x.Id);

        modelBuilder.Entity<User>()
            .HasKey(x => x.Id);

        modelBuilder.Entity<Vote>()
            .HasKey(x => x.Id);

        modelBuilder.Entity<Vote>()
            .Property(x => x.UserId)
            .IsRequired();

        modelBuilder.Entity<Vote>()
            .Property(x => x.PoolOptionId)
            .IsRequired();

        modelBuilder.Entity<Vote>()
            .HasOne(v => v.User)
            .WithMany(u => u.Votes)
            .HasForeignKey(v => v.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<PoolOption>()
           .Property(x => x.PostId)
           .IsRequired();

        modelBuilder.Entity<Post>()
            .HasMany(p => p.Options)
            .WithOne(o => o.Post)
            .OnDelete(DeleteBehavior.Cascade); // Каскадное удаление для связанных PoolOptions

        modelBuilder.Entity<PoolOption>()
            .HasOne(o => o.Post)
            .WithMany(p => p.Options)
            .OnDelete(DeleteBehavior.Cascade); // Каскадное удаление для связанных Votes

        modelBuilder.Entity<Vote>()
            .HasOne(v => v.PoolOption)
            .WithMany(o => o.Votes)
            .OnDelete(DeleteBehavior.Cascade);
    }

}