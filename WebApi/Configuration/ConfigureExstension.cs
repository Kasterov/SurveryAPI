using System.Reflection;
using Application.Configuration;
using Infrastructure.Configuration;
using MediatR;
using Application.Abstractions.Users;
using Infrastructure.Repositories;
using Application.MediatR.Users.Commands;
using Application.Abstractions.Posts;
using Application.Abstractions.Votes;
using Application.Abstractions.Jobs;
using Application.Abstractions.Educations;
using Application.Abstractions.Countries;
using Application.Abstractions.Hobbies;
using Application.Abstractions.Files;
using Application.Abstractions.Complains;
using Application.Abstractions.SavedPosts;
using Application.Abstractions.UserCodes;

namespace WebApi.Configuration;

public static class ConfigureExstension
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetAssembly(typeof(CreateUser))));

        builder.Services.AddScoped<IVoteRepository, VoteRepository>();
        builder.Services.AddScoped<IPostRepository, PostRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IJobRepository, JobRepository>();
        builder.Services.AddScoped<IEducationRepository, EducationRepository>();
        builder.Services.AddScoped<ICountryRepository, CountryRepository>();
        builder.Services.AddScoped<IHobbyRepository, HobbyRepository>();
        builder.Services.AddScoped<IMediaRepository, MediaRepository>();
        builder.Services.AddScoped<IComplainRepository, ComplainRepository>();
        builder.Services.AddScoped<ISavedPostRepository, SavedPostRepository>();
        builder.Services.AddScoped<IUserCodeRepository, UserCodeRepository>();

        builder.Services
            .AddApplication()
            .AddInfrastructure(builder.Configuration);

        var currentAssemblies = AppDomain.CurrentDomain.GetAssemblies();
        builder.Services.AddAutoMapper(currentAssemblies);
    }
}
