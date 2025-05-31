using TempriDomain.Entity;

namespace PrintPdfGenerator.Interfaces;

public interface IBaseSvgGenerator
{
  public Task Generate(PrintMasterEntity print, bool IsTestCase);
}