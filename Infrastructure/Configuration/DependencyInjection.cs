using Application.Abstractions.Common;
using Application.Abstractions.Files;
using Application.Abstractions.UserCodes;
using Application.Abstractions.Users;
using Infrastructure.Db;
using Infrastructure.Interceptors;
using Infrastructure.Services.Code;
using Infrastructure.Services.Mail;
using Infrastructure.Services.Media;
using Infrastructure.Services.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefualtConnection");

        services.AddScoped<IVerifyHashService, VerifyHashService>();
        services.AddScoped<IMediaLinkGeneratorService, MediaLinkGeneratorService>();
        services.AddScoped<IJWTGeneratorService, JWTGeneratorService>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IMailSenderService, MailSenderService>();
        services.AddScoped<ICodeGenerator, CodeGenerator>();
        services.AddScoped<IIdentity, UserIdentity>();

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseSqlServer(connectionString);
        });

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        return services;
    }
}
