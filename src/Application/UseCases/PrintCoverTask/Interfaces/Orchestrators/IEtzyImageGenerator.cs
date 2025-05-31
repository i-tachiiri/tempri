using TempriDomain.Entity;

namespace PrintCoverGenerator.Interfaces.Services;
public interface IEtzyImageGenerator
{
    Task GenerateImage(PrintMasterEntity master);
}