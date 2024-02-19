using Application.Abstractions.Files;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services.Media;

public class MediaLinkGeneratorService : IMediaLinkGeneratorService
{
    private readonly IConfiguration _configuration;

    public MediaLinkGeneratorService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateMediaLink(int? mediaId)
    {
        string? domen = Environment.GetEnvironmentVariable("ASPNETCORE_URLS")?.Split(";")[0];

        if (domen is null || mediaId is null)
        {
            return String.Empty;
        }

        return $"{domen}/File/file-content?id={mediaId}";
    }
}
