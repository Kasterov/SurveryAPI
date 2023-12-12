using Application.Abstractions.Assignments;
using Infrastructure.Db;
using Infrastructure.Repositories.Assignments;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Application.Configuration;
using Infrastructure.Configuration;
using MediatR;
using Application.Mapper;
using Infrastructure.Repositories.Tickets;
using Application.Abstractions.Users;
using Infrastructure.Repositories;
using Application.MediatR.Users.Commands;
using Application.Abstractions.Posts;
using Application.Abstractions.Votes;

namespace WebApi.Configuration;

public static class ConfigureExstension
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetAssembly(typeof(CreateUser))));

        builder.Services.AddScoped<IVoteRepository, VoteRepository>();
        builder.Services.AddScoped<IPostRepository, PostRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();

        builder.Services
            .AddApplication()
            .AddInfrastructure(builder.Configuration);

        builder.Services.AddAutoMapper(typeof(MappingProfile));
    }
}
