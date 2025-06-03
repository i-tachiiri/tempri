using TempriDomain.Entity;
namespace TempriInterfaces.Coordinator;
public interface IPrintCoverUseCase
{
    Task Run(PrintMasterEntity master, bool isTestCase);
}
