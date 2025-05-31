using TempriDomain.Entity;

public interface IQaSheetOpener
{
  public Task OpenQaSheet(PrintMasterEntity print);
}