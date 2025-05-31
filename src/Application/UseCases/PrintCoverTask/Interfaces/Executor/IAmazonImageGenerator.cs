using TempriDomain.Entity;
namespace PrintCoverGenerator.Interfaces.Services;
public interface IAmazonImageGenerator
{
    Task GenerateImage(PrintMasterEntity master);
}