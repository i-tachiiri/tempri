using TempriDomain.Entity;

public interface IWorksheetBuilder
{
  public Task Build(PrintMasterEntity print);
}