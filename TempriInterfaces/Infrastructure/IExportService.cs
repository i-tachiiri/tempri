namespace TempriInterfaces.Infrastructure;

public interface IExportService
{
    Task ExportImage(string downloadUrl, string exportPath);
}