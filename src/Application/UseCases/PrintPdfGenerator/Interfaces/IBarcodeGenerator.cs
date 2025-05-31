using TempriDomain.Entity;

namespace PrintPdfGenerator.Interfaces;

public interface IBarcodeGenerator
{
  public void GenerateBarcodeSvg(PrintMasterEntity print);
}