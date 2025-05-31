namespace PrintCoverGenerator.Interfaces.Infrastructure;

public interface IExportService
{
    Task ExportImage(string downloadUrl, string exportPath);
}