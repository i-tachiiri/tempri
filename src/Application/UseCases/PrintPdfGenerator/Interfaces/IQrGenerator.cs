using TempriDomain.Entity;

namespace PrintPdfGenerator.Interfaces;

public interface IQrGenerator
{
  public void ExportQrCodes(PrintMasterEntity print);
}