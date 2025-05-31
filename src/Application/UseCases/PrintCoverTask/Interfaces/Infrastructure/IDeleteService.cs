namespace PrintCoverGenerator.Interfaces.Infrastructure;

public interface IDeleteService
{
    Task DeleteAllFilesInFolderAsync(string folderId);
}