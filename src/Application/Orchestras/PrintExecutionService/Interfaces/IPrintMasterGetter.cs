using TempriDomain.Entity;
namespace PrintExecutionService.Interfaces;
public interface IPrintMasterGetter
{
    public Task<PrintMasterEntity> GetPrintEntity(int printId);
}

