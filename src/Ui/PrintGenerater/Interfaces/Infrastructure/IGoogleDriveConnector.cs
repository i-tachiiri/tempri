using TempriDomain.Entity;

namespace PrintGenerater.Interfaces.Infrastructure;

public interface IGoogleDriveConnector
{
  Task<string> UploadFile(string filePath, string fileName, string folderId);
  Task<string> CreateFolder(string folderName, string parentFolderId);
  Task<string> GetFolderId(string folderName, string parentFolderId);
  Task DeleteFile(string fileId);
  Task<bool> FileExists(string fileName, string folderId);
  Task<string> CopyFile(string sourceFileId, string destinationFolderId, string newFileName);
}