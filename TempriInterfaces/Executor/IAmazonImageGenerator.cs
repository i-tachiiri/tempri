using TempriDomain.Entity;
namespace TempriInterfaces.Executor;
public interface IAmazonImageGenerator
{
    Task GenerateImage(PrintMasterEntity master);
}