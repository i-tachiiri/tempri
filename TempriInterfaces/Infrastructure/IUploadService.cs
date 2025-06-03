namespace TempriInterfaces.Infrastructure;

public interface IUploadService
{
    Task<string> UploadImage(string localPath, string folderId);
}