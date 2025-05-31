using TempriDomain.Entity;

namespace PrintCoverGenerator.Interfaces.Services;
public interface IEcBaseGenerator
{
    void Convert2Pngs(PrintMasterEntity master);
}