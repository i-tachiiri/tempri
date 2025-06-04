using TempriDomain.Entity;

namespace TempriInterfaces.Executor;
public interface IEtzyImageGenerator
{
    Task GenerateImage(PrintMasterEntity master);
}