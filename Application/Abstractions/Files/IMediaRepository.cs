using Domain.Entities;

namespace Application.Abstractions.Files;

public interface IMediaRepository
{
    public Task<Media?> GetFile(int id);

    public Task<bool> UploadFile(Media file);
}
