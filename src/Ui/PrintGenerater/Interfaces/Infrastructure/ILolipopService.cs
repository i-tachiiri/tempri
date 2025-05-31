using TempriDomain.Entity;

namespace PrintGenerater.Interfaces.Infrastructure;

public interface ILolipopService
{
  Task<bool> UploadFile(string localPath, string remotePath);
  Task<bool> DeleteFile(string remotePath);
  Task<bool> FileExists(string remotePath);
  Task<string> GetPublicUrl(string remotePath);
  Task<bool> CreateDirectory(string remotePath);
  Task<bool> DeleteDirectory(string remotePath);
}