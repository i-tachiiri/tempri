namespace PrintCoverGenerator.Interfaces.Infrastructure;

public interface IUploadService
{
    Task<string> UploadImage(string localPath, string folderId);
}