using Domain.Entities;

namespace Application.Abstractions.Files;

public interface IFileEntityRepository
{
    public Task<FileEntity?> GetFile(int id);

    public Task<bool> UploadFile(FileEntity file);
}
