using TempriDomain.Entity;
namespace PrintPdfGenerator.Interfaces;

public interface IPdfGenerator
{
  public void CreateGoodsPdf(PrintMasterEntity print);
}