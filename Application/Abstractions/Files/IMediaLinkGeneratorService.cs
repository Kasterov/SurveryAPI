namespace Application.Abstractions.Files;

public interface IMediaLinkGeneratorService
{
    public string GenerateMediaLink(int? mediaId);
}
