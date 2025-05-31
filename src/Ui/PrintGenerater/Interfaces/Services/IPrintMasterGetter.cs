public interface IPrintMasterGetter
{
  public Task<IPrintMasterEntity> GetPrintEntity(int printId);
}