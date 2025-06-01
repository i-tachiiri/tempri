using TempriDomain.Entity;
namespace PrintCoverGenerator.Interfaces.UseCases;
public interface IPrintCoverUseCase
{
    Task Run(PrintMasterEntity master, bool isTestCase);
}
