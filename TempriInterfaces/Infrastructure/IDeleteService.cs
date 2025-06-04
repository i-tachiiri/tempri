namespace TempriInterfaces.Infrastructure;

public interface IDeleteService
{
    Task DeleteAllFilesInFolderAsync(string folderId);
}