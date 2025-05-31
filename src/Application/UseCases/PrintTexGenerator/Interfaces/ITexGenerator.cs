using TempriDomain.Entity;

public interface ITexGenerator
{
    public Task Generate(PrintMasterEntity print, bool IsTestCase);
}
