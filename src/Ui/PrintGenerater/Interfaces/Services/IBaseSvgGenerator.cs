namespace PrintGenerater.Interfaces.Services;

public interface IBaseSvgGenerator
{
  public Task Generate(IPrintMasterEntity print, bool IsTestCase);
}